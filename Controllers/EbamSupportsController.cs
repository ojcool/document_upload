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
    public class EbamSupportsController : Controller
    {
        private ebam_app_dbEntities db = new ebam_app_dbEntities();

        // GET: EbamSupports
        public ActionResult Index(int page =1, int pageSize=8)
        {

            List<EbamSupport> listsupport = db.EbamSupports.ToList();
           // var ebamSupports = db.EbamSupports.OrderBy(e => e.AppManager).Include(e => e.AppManager1);
            PagedList<EbamSupport> model = new PagedList<EbamSupport>(listsupport, page, pageSize);
            // ViewBag.TotalStudents = ebamSupports.Count();
            return View(model);

        }

        // GET: EbamSupports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EbamSupport ebamSupport = db.EbamSupports.Find(id);
            if (ebamSupport == null)
            {
                return HttpNotFound();
            }
            return View(ebamSupport);
        }

        // GET: EbamSupports/Create
        public ActionResult Create()
        {
            ViewBag.DateCreated = DateTime.Now;
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager");
            ViewBag.SupportId2 = new SelectList(db.AppManagers, "Id", "Manager");
            return View();
        }

        // POST: EbamSupports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupportId,SupportId2,Duration,StateDate,EndDate")] EbamSupport ebamSupport)
        {
            if (ModelState.IsValid)
            {
                db.EbamSupports.Add(ebamSupport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamSupport.SupportId);
            ViewBag.SupportId2 = new SelectList(db.AppManagers, "Id", "Manager", ebamSupport.SupportId2);
            return View(ebamSupport);
        }

        // GET: EbamSupports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EbamSupport ebamSupport = db.EbamSupports.Find(id);
            if (ebamSupport == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamSupport.SupportId);
            ViewBag.SupportId2 = new SelectList(db.AppManagers, "Id", "Manager", ebamSupport.SupportId2);
            return View(ebamSupport);
        }

        // POST: EbamSupports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupportId,SupportId2,Duration,StateDate,EndDate")] EbamSupport ebamSupport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ebamSupport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamSupport.SupportId);
            ViewBag.SupportId2 = new SelectList(db.AppManagers, "Id", "Manager", ebamSupport.SupportId2);
            return View(ebamSupport);
        }

        // GET: EbamSupports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EbamSupport ebamSupport = db.EbamSupports.Find(id);
            if (ebamSupport == null)
            {
                return HttpNotFound();
            }
            return View(ebamSupport);
        }

        // POST: EbamSupports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EbamSupport ebamSupport = db.EbamSupports.Find(id);
            db.EbamSupports.Remove(ebamSupport);
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
