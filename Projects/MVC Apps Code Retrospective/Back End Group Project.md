#### (FOCUS = BACK-END)
## TECH ACADEMY JOB PLACEMENT DASHBOARD 

### PROJECT OVERVIEW

>The Job Placement Dashboard is an MVC code-first web application which will be used in organizing, tracking, and supporting students through job placement.  My team's sprint involved work on both back-end and front-end development, with a heavier focus on the back-end.  Below is an overview of the work that I completed during this time.

[BACK TO CODE RETROSPECTIVE](Code%20Retrospective.md)

# #

[FRONT-END WORK](#front-end-work)     ||     [MIXED WORK](#mixed-work)

### BACK-END WORK

- [DATABASE SEED METHOD](#database-seed-method)
- [REFRESH MEETUP GROUP EVENTS](#refresh-meetup-group-events)
- [STUDENT PROFILE EDITABILITY](#student-profile-editability)

##### DATABASE SEED METHOD

>I was tasked with developing the Seed method to populate the database with sample data for the code-first Entity Framework setup.  A major focus for me during this task was to write the code in a way that would make increasing the number of records created in the Seed method easy.  

``` C#
namespace JobPlacementDashboard.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using JobPlacementDashboard.Models;
    using JobPlacementDashboard_2.Enums;
    using JobPlacementDashboard_2.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<JobPlacementDashboard.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(JobPlacementDashboard.Models.ApplicationDbContext context)
        {
            // Delete exisitng data from tables before adding seed data
            context.Database.ExecuteSqlCommand("DELETE FROM [JPChecklists]");
            context.Database.ExecuteSqlCommand("DELETE FROM [JPStudents]");
            context.Database.ExecuteSqlCommand("DELETE FROM [JPBulletins]");
            context.Database.ExecuteSqlCommand("DELETE FROM [JPMessages]");
            context.Database.ExecuteSqlCommand("DELETE FROM [JPMeetupGroups]");
            context.Database.ExecuteSqlCommand("DELETE FROM [JPMeetupEvents]");
            context.Database.ExecuteSqlCommand("DELETE FROM [JPOutsideContacts]");
            context.Database.ExecuteSqlCommand("DELETE FROM [JPStudentLocations]");
            context.Database.ExecuteSqlCommand("DELETE FROM [JPApplications]");
            context.Database.ExecuteSqlCommand("DELETE FROM [JPHires]");
            context.Database.ExecuteSqlCommand("DELETE FROM [AspNetUsers]");
            
            // Create a default 'Not Assigned' JPStudentLocation for initializing students
            var locations = new List<JPStudentLocation>
            {
                new JPStudentLocation { Location_id = Guid.NewGuid(), Location_name = "Not Assigned" }
            };
            locations.ForEach(x => context.JPStudentLocations.Add(x));
            context.SaveChanges();

            // Add 5 JPBulletins
            var bulletins = new List<JPBulletin>
            {
                new JPBulletin { Bulletin_category = BulletinCategory.Advice,
                                 Bulletin_body = "This is a short bulletin body." },
                new JPBulletin { Bulletin_category = BulletinCategory.Event,
                                 Bulletin_body = "This is a long bulletin body about an event that we recommend all Tech Academy students go to. " +
                                                 "It will be held at the Portland campus and there will be 5 surprise guest speakers from the Tech industry."},
                new JPBulletin { Bulletin_category = BulletinCategory.JobPost,
                                 Bulletin_body = "You want a job?  Come get it!" },
                new JPBulletin { Bulletin_category = BulletinCategory.Advice,
                                 Bulletin_body = "Here is some pretty handy advice about whatever you want it to be about." },
                new JPBulletin { Bulletin_category = BulletinCategory.Event,
                                 Bulletin_body = "You like baby animals?  Come to this event to snuggle all the fluffy kittens, puppies, and more!" }
            };
            bulletins.ForEach(x => context.JPBulletins.Add(x));
            context.SaveChanges();

            // Add 1 Admin User
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var admin = new ApplicationUser { UserName = "admin0@email.com", Email = "admin0@email.com" };
            manager.Create(admin, "Admin123!");
            manager.AddToRole(admin.Id, "Admin");

            // Add 3 Users w / corresponding JPStudents and JPChecklists + 1 default JPMessage for each
            var users = new List<ApplicationUser>();
            var students = new List<JPStudent>();
            var checklists = new List<JPChecklist>();
            var messages = new List<JPMessage>();

            for (int i = 0; i < 3; i++)
            {
                var email = "user" + i + "@email.com";
                if (!context.Users.Any(u => u.UserName == email))
                {
                    users.Add(new ApplicationUser
                    {
                        UserName = email,
                        Email = email
                    });
                    // Used mods to dynamically create different variations of student completions
                    // This setup is overkill considering only 3 seeds, however it's setting up for expansion of many seeds
                    {
                        ApplicationUser = users[i],
                        Start_date = new DateTime(2018, 01, 01),
                        LinkedIn = (i % 2 == 0) ? "http://" : "",
                        Portfolio = (i < 2) ? "http://" : "",
                        GitHub = "http://",
                        JPContact = (i < 2) ? true : false,
                        Graduated = (i < 2) ? true : false,
                        Hired = (i < 1) ? true : false
                    });
                    checklists.Add(new JPChecklist
                    {
                        JPStudent = students[i],
                        GitHub = (students[i].GitHub == "") ? false : true,
                        LinkedIn = (students[i].LinkedIn == "") ? false : true,
                        Portfolio = (students[i].Portfolio == "") ? false : true,
                        Cover_letter = (i < 2) ? true : false,
                        Resume = (i < 3) ? true : false,
                        Interview_questions = (i < 1) ? true : false,
                        Mock_interview = (i < 1) ? true : false,
                        JPCourse_completion = (i < 2) ? true : false
                    });
                    messages.Add(new JPMessage
                    {
                        Student_id = students[i].Student_id,
                        Graduate = students[i].Graduated,
                        Hire = students[i].Hired,
                        Checklist = true
                    });
                }
            }
            users.ForEach(x => manager.Create(x, "Password123!"));
            students.ForEach(x => context.JPStudents.Add(x));
            checklists.ForEach(x => context.JPChecklists.Add(x));
            messages.ForEach(x => context.JPMessages.Add(x));
            context.SaveChanges();

            // Add 3 JPOutsideContacts
            var outsideContacts = new List<JPOutsideContact>();
            for (int i = 0; i < 3; i++)
            {
                outsideContacts.Add(new JPOutsideContact()
                {
                    Name = "Contact " + i,
                    Position = "Position " + i,
                    Company = "Company " + i,
                    CompanyURL = "http://",
                    LinkedIn = "http://",
                    Location = "Company Address " + i,
                    Contact = "Contact " + i,
                    Stack = "Stack " + i
                });
             }
            outsideContacts.ForEach(x => context.JPOutsideContacts.Add(x));
            context.SaveChanges();

            //Add 3 JPMeetupGroups
            var meetupGroups = new List<JPMeetupGroup>
            {
                new JPMeetupGroup { Group_url = "https://www.meetup.com/Women-Who-Code-Boulder-Denver/" },
                new JPMeetupGroup { Group_url = "https://www.meetup.com/Bootcampers-Collective/" },
                new JPMeetupGroup { Group_url = "https://www.meetup.com/CodeForDenver/" }
             };
            meetupGroups.ForEach(x => context.JPMeetupGroups.Add(x));
            context.SaveChanges();

            //Add 3 JPApplications
            var applications = new List<JPApplication>();
            var n = 3; //# of seed applications
            for (int i = 0; i < n; i++)
            {
                // Used mods to dynamically assign properties, creating variation, but also allowing for multiple students at one company.
                // This setup is overkill considering only 3 seeds, however it's setting up for expansion of many seeds
                applications.Add(new JPApplication()
                {
                    // Dynamically ensures there will be multiple applications for some companies, 
                    // Expands the company pool as # of seed applications grows 
                    Company_name = "Company " + i%(int)Math.Floor(.8*n),
                    Job_title = "Title " + i%3, 
                    JPJob_catagory = i%4, 
                    Company_city = "City " + i % (int)Math.Floor(.8 * n), // For simplicity, city options aligns with company options
                    State_code = "ST",
                    Application_date = DateTime.Now,
                    Student_id = students[i%(students.Count-1)].Student_id, // -1 to leave one student with 0 applications
                    Student_location = locations[i%locations.Count],
                    Heard_back = (i < (int)Math.Floor(.7 * n)) ? true:false, // ~2/3 of applications will have heard back
                    Interview = (i < (int)Math.Floor(.35 * n)) ? true:false // ~half of the heard back applications will have interview
                });
            }
            applications.ForEach(x => context.JPApplications.Add(x));
            context.SaveChanges();

            base.Seed(context);

            //Add 3 JPHires
            Random rnd = new Random(); //used for salary below
            var hires = new List<JPHire>();
            n = 3; // # of seed hires

            for (int i = 0; i < n; i++)
            {
                // Used mods to dynamically assign properties, creating variation, but also allowing for multiple students at one company.
                // This setup is overkill considering only 3 seeds, however it's setting up for expansion of many seeds
                hires.Add(new JPHire()
                {
                    // Dynamically ensures there will be multiple hires for some companies, 
                    // Expands the company pool as # of seed hires grows
                    Company_name = "Company " + i % (int)Math.Floor(.9 * n),
                    Job_title = "Title " + i % 3,
                    JobCategory = (JobCategory)(i% (Enum.GetNames(typeof(JobCategory)).Length)),
                    Salary = rnd.Next(30000, 90000),
                    Company_city = "City " + i % (int)Math.Floor(.9 * n), // For simplicity, city options aligns with company options
                    State_code = "ST",
                    Careers_page = "http://",
                    Hire_date = DateTime.Now,
                    Student_id = students[i % (students.Count - 1)].Student_id, // -1 to leave one student with 0 applications
                    JPStudentLocation = locations[i % locations.Count]
                });
            }
            hires.ForEach(x => context.JPHires.Add(x));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
```

# #

##### REFRESH MEETUP GROUP EVENTS
>As part of the job placement support for students, this application needs to help students find/track tech industry related Meetup groups and display upcoming Events for these groups.  I was tasked with developing code to ensure that meetup events are refreshed upon page load of all events for the relevant Meetup groups.

>The first step was to create a method UpdateMeetups() in the Controller, which gets called upon the page load for displaying the Meetup events.  This method refreshes events that are outdated or no longer relevant.

``` C#
public void UpdateMeetups()
{
    // Find groups which haven't been updated in 24 hours and run RefreshGroup method
    var timeCheck = DateTime.Now.AddHours(-24);
    var groupOutdated = db.JPMeetupGroups.Where(x => x.LastUpdated < timeCheck).ToList();
    foreach (JPMeetupGroup mGroup in groupOutdated) RefreshGroup(mGroup.Group_id);
    db.SaveChanges();

    // Find groups where event date is already past and delete
    foreach (JPMeetupEvent mEvent in db.JPMeetupEvents.Where(x => x.Event_date <= DateTime.Now)) 
    {
        db.JPMeetupEvents.Remove(mEvent);
    }
    db.SaveChanges();

    // Find groups where events still exist, but the group has been removed, and delete all related events
    var groupInactive = (from e in db.JPMeetupEvents select e.Group_id)
                        .Except(from g in db.JPMeetupGroups select g.Group_id).ToList();
    foreach (var groupId in groupInactive)
    {
        foreach (JPMeetupEvent mEvent in db.JPMeetupEvents.Where(x => x.Group_id == groupId))
        {
            db.JPMeetupEvents.Remove(mEvent);
        }
    }
    db.SaveChanges();
}
```

>The second step was to develop the RefreshGroup method, which is called in the first check in the above UpdateMeetups method.  The RefreshGroup method is responsible for actually getting the events from Meetup.com and loading the database with these up to date events.

``` C#
public void RefreshGroup(Guid GroupId)
{
    //Find and delete existing events for the group in the database
    db.JPMeetupEvents.RemoveRange(db.JPMeetupEvents.Where(x => x.Group_id == GroupId));
    db.SaveChanges();

    // Query Meetup for the requested group and get its events
    string groupURL = db.JPMeetupGroups.Find(GroupId).Group_url.TrimEnd('/');
    using (var client = new WebClient())
    {
        NestedMeetupEvent[] meetupEventsVM = JsonConvert.DeserializeObject<NestedMeetupEvent[]>(client.DownloadString(@"https://api.meetup.com/" + groupURL.Remove(0, 23) + @"/events"));

        // for each item in event VM, extract info and add to database
        for (int i = 0; i < meetupEventsVM.Length; i++)
        {
            NestedMeetupEvent eventVM = meetupEventsVM[i];

            Venue venue = eventVM.Venue;

            // Set any null values from the venue to empty for error protection
            var locName = venue.Name ?? "";
            var locAddress = venue.Address_1 ?? "";
            var locCity = venue.City ?? "";
            var locState = venue.State ?? "";
            var locZip = venue.Zip ?? "";

            // Create event and add to database
            var mEvent = new JPMeetupEvent()
            {
                Group_id = GroupId,
                Event_name = eventVM.Name,
                Event_link = eventVM.Link,
                Event_date = DateTime.ParseExact((eventVM.Local_date + " " + eventVM.Local_time), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Location = string.Format("{0}, {1}, {2}, {3} {4}", locName, locAddress, locCity, locState.ToUpper(), locZip),
                Date_created = new DateTime(1970, 1, 1).AddMilliseconds(eventVM.Created)
            };
            db.JPMeetupEvents.Add(mEvent);
        }

        db.JPMeetupGroups.Find(GroupId).LastUpdated = DateTime.Now;
        db.SaveChanges();
    }
}
```

# #

##### STUDENT PROFILE EDITABILITY
 >Each student has a profile on this web application, which both the student and the job placement director use to track what steps the student has completed in the job placement process.  My involvement with this portion of the application was to ensure students are able to edit their own profile and, if they make changes to their graduated or hired status, to create a message notification for the job placement director. 

>The first step was to create an action method which identifies the current user and redirects the application to open an edit page wiht the students profile information.

``` C#
public ActionResult EditProfile()
{
    var userId = User.Identity.GetUserId();
    var studentId = db.JPStudents.Where(x => x.ApplicationUser.Id == userId).FirstOrDefault().Student_id;
    if (studentId == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    return RedirectToAction("Edit", new { id = studentId });
}
```


>The second step was to modify the post Edit method to identify changes made to the student graduated or hired method and add a new message to the database.

``` C#
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Edit([Bind(Include = "Student_id,Start_date,LinkedIn,Portfolio,GitHub,JPContact,Graduated,Hired")] JPStudent jPStudent)
{
    if (ModelState.IsValid)
    {
        var original = db.JPStudents.Find(jPStudent.Student_id);
        var checklist = db.JPChecklists.Where(x => x.JPStudent.Student_id == jPStudent.Student_id).FirstOrDefault();
        if (original.Hired != jPStudent.Hired || original.Graduated != jPStudent.Graduated)
        {
            var message = new JPMessage()
            {
                Student_id = jPStudent.Student_id,
                Graduate = jPStudent.Graduated,
                Hire = jPStudent.Hired,
                Checklist = false
            };
            db.JPMessages.Add(message);
            db.SaveChanges();
        }
        db.Entry(original).CurrentValues.SetValues(jPStudent);
        db.SaveChanges();
        return RedirectToAction("Index");
    }
    return View(jPStudent);
}
```

# #

[PROJECT OVERVIEW](#project-overview)     ||     [BACK-END WORK](#back-end-work)     ||     [MIXED WORK](#mixed-work)

### FRONT-END WORK

##### NAVBAR BY USER-ROLE

>The majority of my involvement in pure front-end development for this project was centered around adding links to the navigation bar, ensuring that the appropriate links were available only to Admins, and to group these together in a dropdown.  On the flip side, there are also a couple of links which are *not* made available to Admins.

``` HTML
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - My ASP.NET Application</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")

</head>
<body>
    <div class="navbar JPnavbarInverse navbar-fixed-top navbar-style">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("Job Placement Dashboard", "Index", "Home", new { area = "" }, new { @class = "navbar-brand navbar-style" })
            </div>
            <div class="navbar-collapse collapse">
                @if (User.IsInRole("Admin"))
                {
                    <div class="btn-group JPnavbarStyling">
                        <button type="button" class="btn">Admin</button>
                        <button type="button" class="btn dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <span class="caret"></span>
                        </button>
                        <ul class="nav dropdown-menu">
                            <li>@Html.ActionLink("Messages", "Index", "JPMessages")</li>
                            <li>@Html.ActionLink("Post Bulletin", "Create", "JPBulletins")</li>
                            <li>@Html.ActionLink("Student Summary", "StudentSummary", "Home")</li>
                        </ul>
                    </div>
                }
                <ul class="nav JPnavbarStyling">
                    @if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        <li>@Html.ActionLink("Home", "Index", "Home")</li>
                        <li>
                            @if (!User.IsInRole("Admin"))
                            {@Html.ActionLink("Edit Profile", "EditProfile", "JPStudents")}
                        </li>
                        <li>
                            @if (!User.IsInRole("Admin"))
                            {@Html.ActionLink("Edit Checklist", "EditMyChecklist", "JPChecklists")}
                        </li>
                        <li>@Html.ActionLink("Job Bulletin", "Index", "JPBulletins")</li>
                        <li>@Html.ActionLink("Job Application", "create", "JPApplications")</li>
                        <li>@Html.ActionLink("Job Offer", "create", "JPHires")</li>
                    }
                    else
                    {
                        <li>@Html.ActionLink("Home", "Index", "Home")</li>
                    }
                </ul>
                @Html.Partial("_LoginPartial")
            </div>
        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @RenderSection("scripts", required: false)
</body>
</html>
```

# #

[PROJECT OVERVIEW](#project-overview)     ||     [BACK-END WORK](#back-end-work)     ||     [FRONT-END WORK](#front-end-work)

### MIXED WORK

##### SUMMARY VIEW

<In this application, the job placement director role needs to be able to quickly see a summary of all students.  One of my peers had already created a view model for the student summary, so I needed to create a controller action result method and a corresponding view file to complete this business requirement.

First, I developed the below StudentSummary method in the appropriate controller.  The method searches the database for all students and creates a student summary view model for each, which is then passed to the student summary view.

``` C#
[Authorize(Roles = "Admin")]
public ActionResult StudentSummary()
{
    IEnumerable<ApplicationUser> users = db.Users.ToList();

    var students = db.JPStudents.ToList();
    var studentVMs = new List<StudentSummaryVM>();
    foreach (var student in students)
    {
        studentVMs.Add(new StudentSummaryVM()
        {
            StudentName = users.Where(x => x.Id == student.ApplicationUser.Id).FirstOrDefault().UserName,
            StudentChecklist = db.JPChecklists.Where(x => x.JPStudent.Student_id == student.Student_id).FirstOrDefault(),
            ApplicationCount = (new JPApplicationsController()).CountApplicationsForStudent(student.Student_id),
            OfferCount = (new JPHiresController()).CountHiresForStudent(student.Student_id),
            Hired = student.Hired,
            Graduated = student.Graduated
        });
    }

    return View(studentVMs);
}
```

In order to populate the application and offer counts above for each student, I also needed to create a couple of quick functions to perform these counts on a given student.

``` C#
// Returns # of applications for a given student id
public int CountApplicationsForStudent(Guid StudentId)
{
    int apps = db.JPApplications.ToList().FindAll(x => x.Student_id == StudentId).Count;
    return apps;
}

// Returns # of applications for a given student id
public int CountHiresForStudent(Guid StudentId)
{
    int hires = db.JPHires.ToList().FindAll(x => x.Student_id == StudentId).Count;
    return hires;
}
```

Finally, I took existing code for the student summary view and updated it so that page would display these student summaries.

``` HTML
@using JobPlacementDashboard.ViewModel
@model List<StudentSummaryVM>

@{ViewBag.Title = "Index";}

@Html.DisplayNameFor(model => model)
    
<h2>Index</h2>
<p>@Html.ActionLink("Create New", "Create")</p>
    
<table class="table">
    <tr>
        <th>@Html.DisplayNameFor(model => model.FirstOrDefault().StudentName)</th>
        <th>@Html.DisplayNameFor(model => model.FirstOrDefault().StudentChecklist)</th>
        <th>@Html.DisplayNameFor(model => model.FirstOrDefault().ApplicationCount)</th>
        <th>@Html.DisplayNameFor(model => model.FirstOrDefault().OfferCount)</th>
        <th>@Html.DisplayNameFor(model => model.FirstOrDefault().Graduated)</th>
        <th>@Html.DisplayNameFor(model => model.FirstOrDefault().Hired)</th>
    </tr>
    @foreach (var item in Model)
    {
        <tr>
            <td>@Html.DisplayFor(modelItem => item.StudentName)</td>
            <td>@Html.DisplayFor(modelItem => item.StudentChecklist)</td>
            <td>@Html.DisplayFor(modelItem => item.ApplicationCount)</td>
            <td>@Html.DisplayFor(modelItem => item.OfferCount)</td>
            <td>@Html.DisplayFor(modelItem => item.Graduated)</td>
            <td>@Html.DisplayFor(modelItem => item.Hired)</td>
        </tr>
    }
</table>
```
