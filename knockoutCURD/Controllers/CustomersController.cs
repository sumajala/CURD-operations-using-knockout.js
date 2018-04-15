using knockoutCURD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace knockoutCURD.Controllers
{
    public class CustomersController : Controller
    {
        private knockoutEntities db = new knockoutEntities();
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Customers.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: customers/Details/5
        public ActionResult Details()
        {
            return PartialView();
        }

        // GET: customers/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public long Create([Bind(Include = "CustomerID,Name,Age,Address,")]Customer customer)
        {
            long id = 0;
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return customer.CustomerID;
            }

            return id;
        }

        // GET: customers/Edit/5
        public ActionResult Edit()
        {
            return PartialView();
        }

        // POST: customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Name,Age,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpStatusCodeResult(200, "Success");
            }
            return new HttpStatusCodeResult(404, "Unable to update.");
        }

        // GET: customers/Delete/5
        public ActionResult Delete()
        {
            return PartialView();
        }

        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "CustomerID,Name,Age,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Deleted;
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
