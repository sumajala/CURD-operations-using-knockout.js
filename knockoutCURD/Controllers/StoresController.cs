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
    public class StoresController : Controller
    {
        private knockoutEntities db = new knockoutEntities();

        // GET: Stores
        

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStores()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Stores.ToList(), JsonRequestBehavior.AllowGet);
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
        public long Create([Bind(Include = "StoreID,Name,Address")] Store store)
        {
            long id = 0;
            if (ModelState.IsValid)
            {
                db.Stores.Add(store);
                db.SaveChanges();
                return store.StoreID;
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
        public ActionResult Edit([Bind(Include = "StoreID,Name,Address")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
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
        public ActionResult DeleteConfirmed([Bind(Include = "StoreID,Name,Address")] Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Deleted;
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