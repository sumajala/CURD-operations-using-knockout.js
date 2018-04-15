using knockoutCURD.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace knockoutCURD.Controllers
{
    public class ProductSoldsController : Controller
    {
        private knockoutEntities db = new knockoutEntities();
        // GET: ProductSolds
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSales()
        {
            var productsold = db.ProductSolds.Include(s => s.Customer).Include(s => s.Product).Include(s => s.Store).Select(x => new
            {
                ID = x.ID,
                ProductID = x.Product.Name,
                CustomerID = x.Customer.Name,
                StoreID = x.Store.Name,
                DateSold = x.DateSold.Day + "- " + x.DateSold.Month + "-" + x.DateSold.Year
            }).ToList();
            return Json(productsold, JsonRequestBehavior.AllowGet);
        }

        // GET:ProductSolds/Details/
        public ActionResult Details(int? id)
        {

            return PartialView();
        }

        // GET: ProductSolds/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            return PartialView();
        }

        // POST: ProductSolds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public int Create([Bind(Include = "ID,ProductID,CustomerID,StoreID,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(productSold);
                db.SaveChanges();
                return productSold.ID;
            }
            else
            {
                return 0;
            }
        }
        // GET: productsold/Edit
        public ActionResult Edit()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "Name");
            return PartialView();
        }

        // POST: productsolds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductID,CustomerID,StoreID,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSold).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpStatusCodeResult(200, "Success");
            }
            return new HttpStatusCodeResult(404, "Unable to update.");
        }

        // GET: productsold/Delete/5
        public ActionResult Delete()
        {
            return PartialView();
        }

        // POST: productsold/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                ProductSold productSold = db.ProductSolds.Find(id);
                db.ProductSolds.Remove(productSold);
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

        private HtmlString getJsonfromObject(object o)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            return new HtmlString(JsonConvert.SerializeObject(o, settings));

        }
        public HtmlString TestQuery()
        {
            var sales = db.ProductSolds.Select(pr => new ProductSold
            {
                ID = pr.ID,
                CustomerID = pr.CustomerID,
                ProductID = pr.ProductID,
                StoreID = pr.StoreID,


            });
            return getJsonfromObject(sales);
        }

    }
}


