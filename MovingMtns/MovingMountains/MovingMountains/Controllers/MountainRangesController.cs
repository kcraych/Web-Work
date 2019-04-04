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
    public class MountainRangesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MountainRanges
        public ActionResult Index()
        {
            return View(db.MountainRanges.ToList());
        }

        // GET: MountainRanges/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MountainRange mountainRange = db.MountainRanges.Find(id);
            if (mountainRange == null)
            {
                return HttpNotFound();
            }
            return View(mountainRange);
        }

        // GET: MountainRanges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MountainRanges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rangeId,rangeName,rangeTerritory,rangeSqMiles")] MountainRange mountainRange)
        {
            if (ModelState.IsValid)
            {
                mountainRange.rangeId = Guid.NewGuid();
                db.MountainRanges.Add(mountainRange);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mountainRange);
        }

        // GET: MountainRanges/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MountainRange mountainRange = db.MountainRanges.Find(id);
            if (mountainRange == null)
            {
                return HttpNotFound();
            }
            return View(mountainRange);
        }

        // POST: MountainRanges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rangeId,rangeName,rangeTerritory,rangeSqMiles")] MountainRange mountainRange)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mountainRange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mountainRange);
        }

        // GET: MountainRanges/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MountainRange mountainRange = db.MountainRanges.Find(id);
            if (mountainRange == null)
            {
                return HttpNotFound();
            }
            return View(mountainRange);
        }

        // POST: MountainRanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MountainRange mountainRange = db.MountainRanges.Find(id);
            db.MountainRanges.Remove(mountainRange);
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
