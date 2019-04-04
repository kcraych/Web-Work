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
    public class ActivityCompletesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityCompletes
        public ActionResult Index()
        {
            return View(db.ActivityCompletes.ToList());
        }

        // GET: ActivityCompletes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityComplete activityComplete = db.ActivityCompletes.Find(id);
            if (activityComplete == null)
            {
                return HttpNotFound();
            }
            return View(activityComplete);
        }

        // GET: ActivityCompletes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityCompletes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "completeId,activityId,dateCompleted,hrsToComplete,myDifficultyScore,myActivityScore,isFavorite,myActivityNotes")] ActivityComplete activityComplete)
        {
            if (ModelState.IsValid)
            {
                activityComplete.completeId = Guid.NewGuid();
                db.ActivityCompletes.Add(activityComplete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityComplete);
        }

        // GET: ActivityCompletes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityComplete activityComplete = db.ActivityCompletes.Find(id);
            if (activityComplete == null)
            {
                return HttpNotFound();
            }
            return View(activityComplete);
        }

        // POST: ActivityCompletes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "completeId,activityId,dateCompleted,hrsToComplete,myDifficultyScore,myActivityScore,isFavorite,myActivityNotes")] ActivityComplete activityComplete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityComplete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityComplete);
        }

        // GET: ActivityCompletes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityComplete activityComplete = db.ActivityCompletes.Find(id);
            if (activityComplete == null)
            {
                return HttpNotFound();
            }
            return View(activityComplete);
        }

        // POST: ActivityCompletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ActivityComplete activityComplete = db.ActivityCompletes.Find(id);
            db.ActivityCompletes.Remove(activityComplete);
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
