using NewsLetterAppMVC.Models;
using NewsLetterAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsLetterAppMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (NewsLetterEntities db = new NewsLetterEntities())
            {
                var signups = db.SignUps.Where(x => x.Removed == null).ToList();
                var signupVms = new List<NewsLetterSignUpVm>();
                foreach (var signup in signups)
                {
                    var signupVm = new NewsLetterSignUpVm();
                    signupVm.Id = signup.Id;
                    signupVm.firstName = signup.FirstName;
                    signupVm.lastName = signup.LastName;
                    signupVm.emailAddress = signup.EmailAddress;
                    signupVms.Add(signupVm);
                }
                return View(signupVms);
            }
        }

        public ActionResult Unsubscribe(int Id)
        {
            using (NewsLetterEntities db = new NewsLetterEntities())
            {
                var signup = db.SignUps.Find(Id);
                signup.Removed = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}