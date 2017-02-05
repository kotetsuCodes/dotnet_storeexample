using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using storeexample.Models;
using storeexample.Utilities;

namespace storeexample.Controllers.Admin
{
    public class AdminStoresController : Controller
    {
        private StoreExampleModel db = new StoreExampleModel();

        // GET: AdminStores
        public ActionResult Index()
        {
            return View(db.Store.ToList());
        }

        // GET: AdminStores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // GET: AdminStores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StoreId,DisplayName,Url,IsOpenOnHolidays,HolidayMessage,DeliveryAvailable,DeliveryFee,DeliveryHourStart,DeliveryHourEnd,IsMultiNational,IsMultiProvince,IsMultiCity,HomeSplashImageUrl,ZipCodeNotInServiceMessage,EmailHost,EmailUsername,EmailPassword,StripePrivateKey,OrderConfirmationFromAddress")] Store store)
        {
            if (ModelState.IsValid)
            {
                var emailEncrypted = store.EmailPassword.GetEncrypted();
                var stripeEncrypted = store.StripePrivateKey.GetEncrypted();

                store.EmailPassword = emailEncrypted.EncryptedString;
                store.EmailPasswordIV = emailEncrypted.IV;
                store.EmailPasswordKey = emailEncrypted.Key;

                store.StripePrivateKey = stripeEncrypted.EncryptedString;
                store.StripeIV = stripeEncrypted.IV;
                store.StripeKey = stripeEncrypted.Key;

                db.Store.Add(store);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(store);
        }

        // GET: AdminStores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: AdminStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StoreId,DisplayName,Url,IsOpenOnHolidays,HolidayMessage,DeliveryAvailable,DeliveryFee,DeliveryHourStart,DeliveryHourEnd,IsMultiNational,IsMultiProvince,IsMultiCity,HomeSplashImageUrl,ZipCodeNotInServiceMessage,EmailHost,EmailUsername,EmailPassword,StripePrivateKey,OrderConfirmationFromAddress")] Store store)
        {
            if (ModelState.IsValid)
            {
                var emailEncrypted = store.EmailPassword.GetEncrypted();
                var stripeEncrypted = store.StripePrivateKey.GetEncrypted();

                store.EmailPassword = emailEncrypted.EncryptedString;
                store.EmailPasswordIV = emailEncrypted.IV;
                store.EmailPasswordKey = emailEncrypted.Key;

                store.StripePrivateKey = stripeEncrypted.EncryptedString;
                store.StripeIV = stripeEncrypted.IV;
                store.StripeKey = stripeEncrypted.Key;

                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(store);
        }

        // GET: AdminStores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Store store = db.Store.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        // POST: AdminStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Store store = db.Store.Find(id);
            db.Store.Remove(store);
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
