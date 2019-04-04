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
    public class TrailDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrailDetails
        public ActionResult Index()
        {
            return View(db.TrailDetails.ToList());
        }

        // GET: TrailDetails/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrailDetail trailDetail = db.TrailDetails.Find(id);
            if (trailDetail == null)
            {
                return HttpNotFound();
            }
            return View(trailDetail);
        }

        // GET: TrailDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrailDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "trailId,activityId,rangeId,landId,trailMiles,trailElevationMax,trailElevationGain,trailNotes,isHikeTrail,isBackpackTrail,isBikeTrail,isSkiTrail,isVisitorTrail")] TrailDetail trailDetail)
        {
            if (ModelState.IsValid)
            {
                trailDetail.trailId = Guid.NewGuid();
                db.TrailDetails.Add(trailDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trailDetail);
        }

        // GET: TrailDetails/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrailDetail trailDetail = db.TrailDetails.Find(id);
            if (trailDetail == null)
            {
                return HttpNotFound();
            }
            return View(trailDetail);
        }

        // POST: TrailDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "trailId,activityId,rangeId,landId,trailMiles,trailElevationMax,trailElevationGain,trailNotes,isHikeTrail,isBackpackTrail,isBikeTrail,isSkiTrail,isVisitorTrail")] TrailDetail trailDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trailDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trailDetail);
        }

        // GET: TrailDetails/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrailDetail trailDetail = db.TrailDetails.Find(id);
            if (trailDetail == null)
            {
                return HttpNotFound();
            }
            return View(trailDetail);
        }

        // POST: TrailDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TrailDetail trailDetail = db.TrailDetails.Find(id);
            db.TrailDetails.Remove(trailDetail);
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
