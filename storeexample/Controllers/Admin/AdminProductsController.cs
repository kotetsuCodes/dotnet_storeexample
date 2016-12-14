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
    public class AdminProductsController : BaseAdminController
    {
        private DonutRunModel db = new DonutRunModel();

        // GET: AdminProducts
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: AdminProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: AdminProducts/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "CategoryName");
            return View();
        }

        // POST: AdminProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,DisplayName,Description,ImageUrl,BasePrice,BaseQuantity,IsActive,IsMultipleItem,IsAlwaysAvailable,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "CategoryName", product.ProductCategoryId);
            return View(product);
        }

        // GET: AdminProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "CategoryName", product.ProductCategoryId);
            return View(product);
        }

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,DisplayName,Description,ImageUrl,BasePrice,BaseQuantity,IsActive,IsMultipleItem,IsAlwaysAvailable,ProductCategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "ProductCategoryId", "CategoryName", product.ProductCategoryId);
            return View(product);
        }

        // GET: AdminProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
