using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLogin.Models;
using PagedList.Mvc;
using PagedList;

namespace AdminLogin.Controllers
{
    public class EbamWorklifeBalsController : Controller
    {
        private ebam_app_dbEntities db = new ebam_app_dbEntities();
        
        public ActionResult Index(int page = 1, int pageSize = 8)
        {

            List<EbamWorklifeBal> listworklife = db.EbamWorklifeBals.ToList();
            PagedList<EbamWorklifeBal> model = new PagedList<EbamWorklifeBal>(listworklife, page, pageSize);
            return View(model);

        }
        // GET: EbamWorklifeBals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EbamWorklifeBal ebamWorklifeBal = db.EbamWorklifeBals.Find(id);
            if (ebamWorklifeBal == null)
            {
                return HttpNotFound();
            }
            return View(ebamWorklifeBal);
        }

        // GET: EbamWorklifeBals/Create
        public ActionResult Create()
        {
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager");
            return View();
        }

        // POST: EbamWorklifeBals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupportId,BalDate,Status")] EbamWorklifeBal ebamWorklifeBal)
        {
            if (ModelState.IsValid)
            {
                db.EbamWorklifeBals.Add(ebamWorklifeBal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamWorklifeBal.SupportId);
            return View(ebamWorklifeBal);
        }

        // GET: EbamWorklifeBals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EbamWorklifeBal ebamWorklifeBal = db.EbamWorklifeBals.Find(id);
            if (ebamWorklifeBal == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamWorklifeBal.SupportId);
            return View(ebamWorklifeBal);
        }

        // POST: EbamWorklifeBals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupportId,BalDate,Status")] EbamWorklifeBal ebamWorklifeBal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ebamWorklifeBal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamWorklifeBal.SupportId);
            return View(ebamWorklifeBal);
        }

        // GET: EbamWorklifeBals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EbamWorklifeBal ebamWorklifeBal = db.EbamWorklifeBals.Find(id);
            if (ebamWorklifeBal == null)
            {
                return HttpNotFound();
            }
            return View(ebamWorklifeBal);
        }

        // POST: EbamWorklifeBals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EbamWorklifeBal ebamWorklifeBal = db.EbamWorklifeBals.Find(id);
            db.EbamWorklifeBals.Remove(ebamWorklifeBal);
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
