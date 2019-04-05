using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Moving_Mountains.Models;

namespace Moving_Mountains.Controllers
{
    public class UserDemographicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserDemographics
        public ActionResult Index()
        {
            return View(db.UserDemographics.ToList());
        }

        // GET: UserDemographics/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDemographic userDemographic = db.UserDemographics.Find(id);
            if (userDemographic == null)
            {
                return HttpNotFound();
            }
            return View(userDemographic);
        }

        // GET: UserDemographics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDemographics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userId,locationId")] UserDemographic userDemographic)
        {
            if (ModelState.IsValid)
            {
                db.UserDemographics.Add(userDemographic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDemographic);
        }

        // GET: UserDemographics/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDemographic userDemographic = db.UserDemographics.Find(id);
            if (userDemographic == null)
            {
                return HttpNotFound();
            }
            return View(userDemographic);
        }

        // POST: UserDemographics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,locationId")] UserDemographic userDemographic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDemographic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDemographic);
        }

        // GET: UserDemographics/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDemographic userDemographic = db.UserDemographics.Find(id);
            if (userDemographic == null)
            {
                return HttpNotFound();
            }
            return View(userDemographic);
        }

        // POST: UserDemographics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserDemographic userDemographic = db.UserDemographics.Find(id);
            db.UserDemographics.Remove(userDemographic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
