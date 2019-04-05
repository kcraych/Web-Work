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
    public class ActivityPhotosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ActivityPhotos
        public ActionResult Index()
        {
            return View(db.ActivityPhotos.ToList());
        }

        // GET: ActivityPhotos/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityPhoto activityPhoto = db.ActivityPhotos.Find(id);
            if (activityPhoto == null)
            {
                return HttpNotFound();
            }
            return View(activityPhoto);
        }

        // GET: ActivityPhotos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivityPhotos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "photoId,completeId,myActivityPhoto")] ActivityPhoto activityPhoto)
        {
            if (ModelState.IsValid)
            {
                activityPhoto.photoId = Guid.NewGuid();
                db.ActivityPhotos.Add(activityPhoto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(activityPhoto);
        }

        // GET: ActivityPhotos/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityPhoto activityPhoto = db.ActivityPhotos.Find(id);
            if (activityPhoto == null)
            {
                return HttpNotFound();
            }
            return View(activityPhoto);
        }

        // POST: ActivityPhotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "photoId,completeId,myActivityPhoto")] ActivityPhoto activityPhoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activityPhoto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activityPhoto);
        }

        // GET: ActivityPhotos/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityPhoto activityPhoto = db.ActivityPhotos.Find(id);
            if (activityPhoto == null)
            {
                return HttpNotFound();
            }
            return View(activityPhoto);
        }

        // POST: ActivityPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ActivityPhoto activityPhoto = db.ActivityPhotos.Find(id);
            db.ActivityPhotos.Remove(activityPhoto);
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
