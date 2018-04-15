using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using knockoutCURD.Models;

namespace knockoutCURD.Controllers
{
    public class ProductsController : Controller
    {
        private knockoutEntities db = new knockoutEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

     
        public JsonResult GetProducts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Products.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Stores/Details/5
        public ActionResult Details()
        {
            return PartialView();
        }

        // GET: Stores/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Stores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public long Create([Bind(Include = "ProductID,Name,Price")] Product product)
        {
            long id = 0;
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return product.ProductID;
            }

            return id;
        }

        // GET: Stores/Edit/5
        public ActionResult Edit()
        {
            return PartialView();
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Price")] Product product
            )
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpStatusCodeResult(200, "Success");
            }
            return new HttpStatusCodeResult(404, "Unable to update.");
        }

        // GET: Stores/Delete/5
        public ActionResult Delete()
        {
            return PartialView();
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "ProductID,Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Deleted;
                db.SaveChanges();
                return new HttpStatusCodeResult(200, "Success");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
