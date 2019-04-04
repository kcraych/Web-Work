using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovingMountains.Models;

namespace MovingMountains.Controllers
{
    public class ActivityDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityDetails
        public ActionResult Index()
        {
            return View(db.ActivityDetails.ToList());
        }

        // GET: ActivityDetails/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityDetail activityDetail = db.ActivityDetails.Find(id);
            if (activityDetail == null)
            {
                return HttpNotFound();
            }
            return View(activityDetail);
        }

        // GET: ActivityDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "activityId,locationId,driveHrsFromHome,activityType,activityOtherDesc,activityName,activityNotes,activityWinter,activitySpring,activitySummer,activityFall,activitySiteName,activitySiteURL")] ActivityDetail activityDetail)
        {
            if (ModelState.IsValid)
            {
                activityDetail.activityId = Guid.NewGuid();
                db.ActivityDetails.Add(activityDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityDetail);
        }

        // GET: ActivityDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityDetail activityDetail = db.ActivityDetails.Find(id);
            if (activityDetail == null)
            {
                return HttpNotFound();
            }
            return View(activityDetail);
        }

        // POST: ActivityDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "activityId,locationId,driveHrsFromHome,activityType,activityOtherDesc,activityName,activityNotes,activityWinter,activitySpring,activitySummer,activityFall,activitySiteName,activitySiteURL")] ActivityDetail activityDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityDetail);
        }

        // GET: ActivityDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityDetail activityDetail = db.ActivityDetails.Find(id);
            if (activityDetail == null)
            {
                return HttpNotFound();
            }
            return View(activityDetail);
        }

        // POST: ActivityDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ActivityDetail activityDetail = db.ActivityDetails.Find(id);
            db.ActivityDetails.Remove(activityDetail);
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
