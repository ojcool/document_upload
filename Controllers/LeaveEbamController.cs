using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLogin.Models;
using PagedList;
using PagedList.Mvc;
namespace AdminLogin.Controllers
{
    public class LeaveEbamController : Controller
    {
        private ebam_app_dbEntities db = new ebam_app_dbEntities();

        // GET: LeaveEbam
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? selectedName)    
        {
            //var leaveEbamLists = db.LeaveEbamLists.Include(l => l.AppManager);
            //return View(leaveEbamLists.ToList());
            var getAppManagerslist = db.AppManagers.OrderBy(q => q.Manager).ToList();
            ViewBag.selectedName = new SelectList(getAppManagerslist, "SupportId", "Manager", selectedName);
            int NameID = selectedName.GetValueOrDefault();

            IQueryable<EbamApp> ebamApps = db.EbamApps
                .Where(c => !selectedName.HasValue || c.SupportId == NameID)
                .OrderBy(d => d.SupportId)
                .Include(d => d.AppManager);
            var sql = ebamApps.ToString();

            ViewBag.CurrentSort = sortOrder;
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var leaves = from l in db.LeaveEbamLists select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                leaves = leaves.Where(l => l.AppManager.Manager.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    leaves = leaves.OrderByDescending(l => l.AppManager.Manager);
                    break;
                case "date_desc":
                    leaves = leaves.OrderByDescending(l => l.StartDate);
                    break;
                default:
                    leaves = leaves.OrderBy(l => l.StartDate);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(leaves.ToPagedList(pageNumber, pageSize));

        }

        // GET: LeaveEbam/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveEbamList leaveEbamList = db.LeaveEbamLists.Find(id);
            if (leaveEbamList == null)
            {
                return HttpNotFound();
            }
            return View(leaveEbamList);
        }

        // GET: LeaveEbam/Create
        public ActionResult Create()
        {
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager");
            return View();
        }

        // POST: LeaveEbam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveEbamList leaveEbamList)
        {
            try
            {
                if (ModelState.IsValid)
            {
                db.LeaveEbamLists.Add(leaveEbamList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", leaveEbamList.SupportId);
            return View(leaveEbamList);
        }

        // GET: LeaveEbam/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveEbamList leaveEbamList = db.LeaveEbamLists.Find(id);
            if (leaveEbamList == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", leaveEbamList.SupportId);
            return View(leaveEbamList);
        }

        // POST: LeaveEbam/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveEbamList leaveEbamList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaveEbamList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", leaveEbamList.SupportId);
            return View(leaveEbamList);
        }

        // GET: LeaveEbam/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveEbamList leaveEbamList = db.LeaveEbamLists.Find(id);
            if (leaveEbamList == null)
            {
                return HttpNotFound();
            }
            return View(leaveEbamList);
        }

        // POST: LeaveEbam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LeaveEbamList leaveEbamList = db.LeaveEbamLists.Find(id);
            db.LeaveEbamLists.Remove(leaveEbamList);
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
