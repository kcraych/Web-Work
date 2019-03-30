#### (FOCUS = FRONT-END)
## TECH ACADEMY SCHEDULING TOOL 

### PROJECT OVERVIEW

>The Scheduling Tool is an MVC code-first web application which will be used in managing employee and student schedules, including work-shift schedules, study schedules, time off schedules, and clock-in/out tracking.  My team's sprint involved work on both back-end and front-end development, with a heavier focus on the front-end.  Below is an overview of the work that I completed during this time.

[BACK TO CODE RETROSPECTIVE](Code%20Retrospective.md)

# #

### PROJECT WORK

- [CREATE A SCHEDULE](#create-a-schedule)
- [EDIT A SCHEDULE](#edit-a-schedule)
- [TABLE SORTING FUNCTIONALITY](#table-sorting-functionality)
- [EMAIL RECIPIENTS MODAL](#email-recipients-modal)
- [NAVIGATION BAR ANIMATIONS](#navigation-bar-animations)

# #

##### CREATE A SCHEDULE

> A large focus for me during this project was making the pages to create a schedule both functional and pleasing to the eye.  While code already existed for the view by the time I entered the project, there were issues with the binding to the models/database and the overall appearance of the page.  The page consists of defining start/end dates for which the schedule will be effective and then setting start/end times for each day of the week.  
> 
> First, I refactored the code so that the bindings to the models worked efficiently.  To do this, I incorporated the use of HTML helpers to create the bindings.  
> 
> Once the view was functional, I worked on the visual aspects of the page.  In order to make the page appear concise, I created a collapse/expand functionality for each day of the week using bootstraps accordian styling.    
> 
> Below is the front-end code with these updates completed.

``` CSHTML
@model Schedule_It_2.Models.Schedule

@{
    ViewBag.Title = "Create";
}

<h2 class="text-center font-tech__blue">Create <small>- Schedule</small></h2>

<div class="row">
    <div class="col create-input font-tech__blue">
        @using (Html.BeginForm("Create", "Schedule", FormMethod.Post))
        {
        <form>
            <div class="panel panel-tech">
                <div class="panel-body">
                    <div class="col-md-12 form-group form-check">
                        <input name="Permanent" type="checkbox" class="form-check-input" id="permanent" value="true">
                        <label class="form-check-label" for="permanent">Permanent?</label>
                    </div>
                    <div class="col-md-4 form-group">
                        @Html.LabelFor(model => model.ScheduleStartDay)
                        @Html.EditorFor(model => model.ScheduleStartDay, new { htmlAttributes = new { @class = "form-control", type = "date" } })
                        @Html.ValidationMessageFor(model => model.ScheduleStartDay, "", new { @class = "text-danger" })
                        <small class="form-text text-muted">Enter the date you want this schedule to be effective.</small>
                    </div>
                    <div class="col-md-4 form-group">
                        @Html.LabelFor(model => model.ScheduleEndDay)
                        @Html.EditorFor(model => model.ScheduleEndDay, new { htmlAttributes = new { @class = "form-control", type = "date" } })
                        @Html.ValidationMessageFor(model => model.ScheduleEndDay, "", new { @class = "text-danger" })
                        <small class="form-text text-muted">Enter the date you want this schedule to end.</small>
                    </div>
                    <div class="col-md-4 form-group">
                        @Html.LabelFor(model => model.Notes)
                        @Html.EditorFor(model => model.Notes, new { htmlAttributes = new { @class = "form-control" } })
                        @Html.ValidationMessageFor(model => model.Notes, "", new { @class = "text-danger" })
                    </div>
                </div>
            </div>

            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                @* info for each day is bound to an element in an array that gets sent back to the controller *@
                <div class="panel panel-tech">
                    <div class="panel-heading panel-tech__header" role="tab" id="headingOne">
                        <h4 class="panel-title panel-tech__title">
                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                Monday
                            </a>
                        </h4>
                    </div>
                    <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                        <div class="panel-body">
                            <div class="col-md-12 form-group form-check">
                                @Html.CheckBoxFor(model => model.ScheduledWorkPeriods[0].IsDayOff, new { htmlAttributes = new { @class = "form-check-input" } })
                                @Html.LabelFor(model => model.ScheduledWorkPeriods[0].IsDayOff, new { htmlAttributes = new { @class = "form-check-label" } })
                            </div>
                            <div class="col-md-6 form-group">
                                @Html.LabelFor(model => model.ScheduledWorkPeriods[0].StartTime)
                                @Html.EditorFor(model => @model.ScheduledWorkPeriods[0].StartTime, new { htmlAttributes = new { @class = "form-control", type = "time" } })
                                <small id="timeHelp" class="form-text text-muted">Enter the time you start for this day.</small>
                            </div>
                            <div class="col-md-6 form-group">
                                @Html.LabelFor(model => model.ScheduledWorkPeriods[0].EndTime)
                                @Html.EditorFor(model => model.ScheduledWorkPeriods[0].EndTime, new { htmlAttributes = new { @class = "form-control", type = "time" } })
                            </div>
                            <div>
                                @Html.EditorFor(model => model.ScheduledWorkPeriods[0].Day, new { htmlAttributes = new { type = "hidden", @Value = "Mon" } })
                            </div>
                        </div>
                    </div>
                </div>

                @*Similar code as above for the "Monday" code is given for Tuesday-Sunday.*@
                @*For simplicity, I've excluded it here.*@ 

            </div>

            <button type="submit" class="btn btn-primary create-button">Submit</button>

        </form>
        }

    </div>
</div>

<br/>

<div>
    @Html.ActionLink("Back to List", "Index")
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}
```

> I also incorporated simple styling changes to the accordian panels to better match the Tech Academy color schemes.

``` CSS
.panel-tech {
    border-color: #ddd;
    background-color: #f4f4f4;
}

.panel-tech__header {
    background-color: lightgray;
    color: #004271;
}

.panel-tech__title {
    font-weight: 600;
}
```

> In addition to the front end work, fixing the binding issues also required a fair amount of backend debugging on existing code for the ScheduleController Create method.  I wound up re-writting this method to fix the bugs and really simplify the logic within it.  Below is the result.

``` C#
[HttpPost]
public ActionResult Create(Schedule schedule)
{
    //if a schedule is created directly from the create view without a user id it breaks the index view 
    if (ModelState.IsValid)
    {
        schedule.ScheduleId = Guid.NewGuid();
        schedule.UserId = HttpContext.User.Identity.GetUserId();

        foreach (var wp in schedule.ScheduledWorkPeriods)
        {
            wp.ScheduleId = schedule.ScheduleId;
            db.ScheduledWorkPeriods.Add(wp);
        }
        db.Schedules.Add(schedule);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    return View();
}
```

[BACK TO TOP](#project-overview)

# #

###### EDIT A SCHEDULE

> The page to edit a schedule followed naturally from the create a schedule page, with a few updates for the edit functionality.  One thing to note is that there are two models involved here: (1) a Schedule, which contains the effective start/end dates, plus a few additional properites such as schedule user and notes; and (2) a ScheduledWorkPeriod, where each workperiod represents the schedule for one day of the week, and is associated with a given Schedule.  
> 
> While it is possible to bind just the Schedule for the Create view and still be able to create associated ScheduledWorkPeriods in the ScheduleController Create method, that is not the case for the Edit view.  For editing, I need to be able to bind an existing Schedule, plus the list of ScheduledWorkPeriods associated with that Schedule.  To do this, I created a view model which consisted of those two properties (a Schedule object and a list of ScheduleWorkPeriod objects).  With this I was able to pass the necessary instances of each model to the view.
>
> One additional hurdle was to ensure I was able to bind the correct ScheduledWorkPeriod to user input for each day of the week on 
the front end.  To achieve this, I created a dictionary to pass to the view via the ViewBag, where the key/value pairs are the the Day/ShiftId pairs representing work periods for each day of the week for the given schedule (the Day property of the ScheduledWorkPeriod is defined by an enum).
> 
> Below is the ScheduleController Edit (HTTP Post) method with these additions.

``` C#
public ActionResult Edit(string id)
{
    Schedule schedule = db.Schedules.Find(id);

    //create VM to pass into edit view in order to easily edit the work periods along with the schedule
    var scheduledWorkPeriods = db.ScheduledWorkPeriods.Where(x => x.ScheduleId == schedule.ScheduleId).ToList();
    var scheduleEditVM = new ScheduleEditVM
    {
        Schedule = schedule,
        ScheduledWorkPeriods = scheduledWorkPeriods
    };

    //create dictionary of the string for the Day enum with the corresponding ShiftId for view binding
    var days = new Dictionary<string, Guid>();
    foreach (var wp in scheduledWorkPeriods)
    {
        days.Add(Enum.GetName(typeof(Days), wp.Day), wp.ShiftId);
    }
    ViewBag.days = days;

    return View(scheduleEditVM);
}
```

> There were also a few changes that needed to be made to the front-end code for the edit page, in comparison to the create page.  The main differences here are the use of the dictionary passed through the ViewBag, to be used in lambda expressions to identify the appropriate ScheduledWorkPeriod to be edited.  Below is an example of the Monday section of the Edit page, showing changes to the binding when compared to the create view.

``` CSHTML
@model Schedule_It_2.Models.ScheduleEditVM

var daysDict = ViewBag.days as Dictionary<string, Guid>;
Guid shiftId;

<div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
    @* info for each day is bound to an element in an array that gets sent back to the controller *@
    <div class="panel panel-default">
        <div class="panel-heading" role="tab" id="headingOne">
            <h4 class="panel-title">
                <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                    Monday
                </a>
            </h4>
        </div>
        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
            <div class="panel-body">
                @{
                    shiftId = daysDict["Mon"];
                    <div class="col-md-12 form-group form-check">
                        @Html.EditorFor(model => model.ScheduledWorkPeriods.Find(x => x.ShiftId == shiftId).IsDayOff, new { htmlAttributes = new { @class = "form-check-input" } })
                        @Html.LabelFor(model => model.ScheduledWorkPeriods.Find(x => x.ShiftId == shiftId).IsDayOff, new { htmlAttributes = new { @class = "form-check-label" } })
                    </div>
                    <div class="col-md-6 form-group">
                        @Html.LabelFor(model => model.ScheduledWorkPeriods.Find(x => x.ShiftId == shiftId).StartTime)
                        @Html.EditorFor(model => @model.ScheduledWorkPeriods.Find(x => x.ShiftId == shiftId).StartTime, new { htmlAttributes = new { @class = "form-control", type = "time" } })
                        <small id="timeHelp" class="form-text text-muted">Enter the time you start for this day.</small>
                    </div>
                    <div class="col-md-6 form-group">
                        @Html.LabelFor(model => model.ScheduledWorkPeriods.Find(x => x.ShiftId == shiftId).EndTime)
                        @Html.EditorFor(model => model.ScheduledWorkPeriods.Find(x => x.ShiftId == shiftId).EndTime, new { htmlAttributes = new { @class = "form-control", type = "time" } })
                    </div>
                }
            </div>
        </div>
    </div>
</div>
```

[BACK TO TOP](#project-overview)

# #

##### TABLE SORTING FUNCTIONALITY

> Throughout the web application, there are multiple Index views which show a table summary of the existing instances for the given view model.  I was tasked with creating javascript code which can be used to add asc/desc sorting to any column in any table.  Below is the javascript function.

``` JavaScript
//input n represents which column to sort on the table and in which direction
function sortTable(n, tblId, dir) {
    //define variables and set initial values
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById(tblId);
    switching = true;

    //upon sort click, loop until either a switch is deemed unecessary or, if necessary, a sort is complete
    while (switching) {
        switching = false;
        rows = table.rows;

        //for given column, sequentially get adjacent row values and check whether a switch is needed.
        //If so, perform switch and START OVER sequentially comparing adjacent row values from 1.
        //This continues until comparison from 1 through all rows results in no switch needed.
        for (i = 1; i < (rows.length - 1); i++) {
            x = rows[i].getElementsByTagName("td")[n].innerHTML.toLowerCase();
            y = rows[i + 1].getElementsByTagName("td")[n].innerHTML.toLowerCase();

            //compare adjacent row values and determine whether switching (asc vs desc) is necessary
            shouldSwitch = false;
            if (dir == "asc") {
                if (x > y) {
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x < y) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        
        //if switch is determined necessary, follow through with appropriate sort
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        } 
    }
    
    //gray out arrow icon that was just used, darken arrow icon not used
    if (dir == "asc") { dir_inv = "desc" }
    else if (dir == "desc") {dir_inv = "asc"}
    rows[0].getElementsByClassName(dir)[n].style.color = "#004271";
    rows[0].getElementsByClassName(dir_inv)[n].style.color = "#cccccc";
}
```

> Additionally, below is an example of html code where this functionality is put to use.

``` CSHTML
@model IEnumerable<Schedule_It_2.Models.TimeOffEvent>

<table class="table" id="toeTable">
    <tr class="font-tech__blue">
        <th>
            @Html.DisplayNameFor(model => model.Start)
            <div class="tbl-sort">
                <span class="asc" onclick="sortTable(0, 'toeTable', 'asc')"><small>&#x25B2;</small></span>
                <span class="desc" onclick="sortTable(0, 'toeTable', 'desc')"><small>&#x25BC;</small></span>
            </div>
        </th>
        <th>@Html.DisplayNameFor(model => model.End)</th>
        <th>@Html.DisplayNameFor(model => model.Note)</th>
        <th>@Html.DisplayNameFor(model => model.Title)</th>
        <th>@Html.DisplayNameFor(model => model.ActiveSchedule)</th>
        <th>
            @Html.DisplayNameFor(model => model.Submitted)
            <div class="tbl-sort">
                <span class="asc" onclick="sortTable(5, 'toeTable', 'asc')"><small>&#x25B2;</small></span>
                <span class="desc" onclick="sortTable(5, 'toeTable', 'desc')"><small>&#x25BC;</small></span>
            </div>
        </th>
        <th></th>
    </tr>
    
    @foreach (var item in Model)
    {
        <tr>
            <td>@Html.DisplayFor(modelItem => item.Start)</td>
            <td>@Html.DisplayFor(modelItem => item.End)</td>
            <td>@Html.DisplayFor(modelItem => item.Note)</td>
            <td>@Html.DisplayFor(modelItem => item.Title)</td>
            <td>@Html.DisplayFor(modelItem => item.ActiveSchedule)</td>
            <td>@Html.DisplayFor(modelItem => item.Submitted)</td>
        </tr>
    }
</table>
```

> Finally, I included the below styling to ensure the inclusion of the sorting arrows was visually appealing.

``` CSS
table th .table-sort {   
    float: left;
}

table th span {
    cursor: pointer;
    color: #cccccc;
}
```

[BACK TO TOP](#project-overview)

# #

##### EMAIL RECIPIENTS MODAL

> In this application, there is a messages page where a user can draft an email message and then select any combination of users to recieve the message.  This code was already largely existing, however the dropdown list to select email recipients was not user friendly.  The list would extend past the viewing area and the user was unable to easily scroll to see the full list.  My goal was to execute a solution that made this experience more user friendly.  I determined the most elegant, user friendly solution would be to change the dropdown list to a modal. 
> 
> Below is the HTML code specifically for this modal on the page.

``` CSHTML
@model Schedule_It_2.Models.Message

<div class="form-group">
    <label class="control-label col-md-2">Recipients</label>
    <div class="col-md-1">
        <button type="button" class="btn btn-default btn-block" data-toggle="modal" data-target="#MessageModal">Edit</button>
    </div>
    <div class="col-md-8">
        <p><span id="recipients"></span></p>
    </div>
    @*recipient selection list modal*@
    <div class="modal" id="MessageModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Select Message Recipients:</h5>
                </div>
                <div class="modal-body">
                    <ul class="user-list" id="recipientList">
                        <!--for each user create li with username and check box-->
                        @for (var x = 0; x < ViewBag.users.Count; x++)
                        {
                            <li class="user-list__item">
                                <a>@ViewBag.users[x].UserName</a>
                                <input type="checkbox" value="@ViewBag.users[x].Id" name="RecipientList" />
                            </li>
                        }
                    </ul>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                    <button type="button" id="saveRecipients" onclick="prepareRecipients()" class="btn btn-primary">Save</button>
                </div>
            </div>
        </div>
    </div>
</div>
```

> I included the below CSS to ensure the modal window always fits within user view window, to make the modal content scrollable if it is longer than the user window allows, and to generally add clean styles to the modal. 

``` CSS
.modal {
    display: none;
    margin: auto;
    margin-top: 50px;
}

.modal-body {
    overflow-y: auto;
    max-height: calc(90vh - 200px);
    min-height: 50px;
}

.modal-content {
    padding: 10px;
}

.modal-footer {
    display: flex;
    justify-content: space-around;
}

.user-list__item {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    padding: 0.5em;
}
```

> Finally, the selected recipient list from the modal should display on the message view after the user saves and exists the modal.

``` Javascript
//grabs children of recipientList > li. Gets Username from "x.children[0]"
//checks if user is selected with "x.children[1].checked"
$(function () {
    let recipients = $('#recipientList li');
    $('#saveRecipients').click(function () {
        let selectedUsers = [];
        for (let x of recipients) {
            if (x.children[1].checked) {
                selectedUsers.push(x.children[0].innerHTML);
            }
        }
        $('#recipients').html(selectedUsers.join(', '));
        $('#MessageModal').modal('hide');
    })
});
```

[BACK TO TOP](#project-overview)

# #

##### NAVIGATION BAR ANIMATIONS

> While the navigation bar was already up and running when I joined in this project, I was tasked with adding some animation to the elements.
> 
> First, I added the below CSS and JavaScript so that the entire navbar slides down on page load, using the navbar margin as the driver for the effect.

``` CSS
#animate-nav {
    margin-top: -200px;
    transition: margin-top 2s;
}

#animate-nav.shown {
    margin-top: 0px;
}
```

``` JavaScript
$(function () {
    var nav = document.getElementById('animate-nav');
    nav.classList.toggle('shown');
});
```

> I also included the below JavaScript so that the navbar dropdown options have a slide in/out effect when clicked.

``` JavaScript
// Add slideDown animation to Bootstrap dropdown when expanding.
$('.dropdown').on('show.bs.dropdown', function () {
    $(this).find('.dropdown-menu').first().stop(true, true).slideDown();
});

// Add slideUp animation to Bootstrap dropdown when collapsing.
$('.dropdown').on('hide.bs.dropdown', function () {
    $(this).find('.dropdown-menu').first().stop(true, true).slideUp();
});
```

[BACK TO TOP](#project-overview)
