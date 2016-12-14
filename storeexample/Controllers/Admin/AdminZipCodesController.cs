using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using donutrun.Models;

namespace donutrun.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminZipCodesController : BaseAdminController
    {
        private DonutRunModel db = new DonutRunModel();

        // GET: AdminZipCodes
        public ActionResult Index()
        {
            return View(db.ZipCodes.ToList());
        }

        // GET: AdminZipCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCode zipCode = db.ZipCodes.Find(id);
            if (zipCode == null)
            {
                return HttpNotFound();
            }
            return View(zipCode);
        }

        // GET: AdminZipCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminZipCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ZipCodeId,Zip,IsServiced")] ZipCode zipCode)
        {
            if (ModelState.IsValid)
            {
                db.ZipCodes.Add(zipCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zipCode);
        }

        // GET: AdminZipCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCode zipCode = db.ZipCodes.Find(id);
            if (zipCode == null)
            {
                return HttpNotFound();
            }
            return View(zipCode);
        }

        // POST: AdminZipCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ZipCodeId,Zip,IsServiced")] ZipCode zipCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zipCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zipCode);
        }

        // GET: AdminZipCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCode zipCode = db.ZipCodes.Find(id);
            if (zipCode == null)
            {
                return HttpNotFound();
            }
            return View(zipCode);
        }

        // POST: AdminZipCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZipCode zipCode = db.ZipCodes.Find(id);
            db.ZipCodes.Remove(zipCode);
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
