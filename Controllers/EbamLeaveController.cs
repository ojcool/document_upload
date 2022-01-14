using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminLogin.Models;

namespace AdminLogin.Controllers
{
    public class EbamLeaveController : Controller
    {
        private ebam_app_dbEntities db = new ebam_app_dbEntities();

        // GET: EbamLeave
        public async Task<ActionResult> Index()
        {
            var leaveEbams = db.LeaveEbams.Include(l => l.AppManager).Include(l => l.AppManager1);
            return View(await leaveEbams.ToListAsync());
        }

        // GET: EbamLeave/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveEbam leaveEbam = await db.LeaveEbams.FindAsync(id);
            if (leaveEbam == null)
            {
                return HttpNotFound();
            }
            return View(leaveEbam);
        }

        // GET: EbamLeave/Create
        public ActionResult Create()
        {
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager");
            ViewBag.SupportId2 = new SelectList(db.AppManagers, "Id", "Manager");
            return View();
        }

        // POST: EbamLeave/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,SupportId,SupportId2,OutstandLeave,StartDate,EndDate,NoOfDays,RemainDays,BackUpStaff")] LeaveEbam leaveEbam)
        {
            if (ModelState.IsValid)
            {
                db.LeaveEbams.Add(leaveEbam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", leaveEbam.SupportId);
            ViewBag.SupportId2 = new SelectList(db.AppManagers, "Id", "Manager", leaveEbam.SupportId2);
            return View(leaveEbam);
            //if (ModelState.IsValid)
            //{
            //    db.LeaveEbams.Add(leaveEbam);
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", leaveEbam.SupportId);
            //ViewBag.SupportId2 = new SelectList(db.AppManagers, "Id", "Manager", leaveEbam.SupportId2);
            //return View(leaveEbam);
        }

        // GET: EbamLeave/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveEbam leaveEbam = await db.LeaveEbams.FindAsync(id);
            if (leaveEbam == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", leaveEbam.SupportId);
            ViewBag.SupportId2 = new SelectList(db.AppManagers, "Id", "Manager", leaveEbam.SupportId2);
            return View(leaveEbam);
        }

        // POST: EbamLeave/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,SupportId,SupportId2,OutstandLeave,StartDate,EndDate,NoOfDays,RemainDays,BackUpStaff")] LeaveEbam leaveEbam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaveEbam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", leaveEbam.SupportId);
            ViewBag.SupportId2 = new SelectList(db.AppManagers, "Id", "Manager", leaveEbam.SupportId2);
            return View(leaveEbam);
        }

        // GET: EbamLeave/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeaveEbam leaveEbam = await db.LeaveEbams.FindAsync(id);
            if (leaveEbam == null)
            {
                return HttpNotFound();
            }
            return View(leaveEbam);
        }

        // POST: EbamLeave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LeaveEbam leaveEbam = await db.LeaveEbams.FindAsync(id);
            db.LeaveEbams.Remove(leaveEbam);
            await db.SaveChangesAsync();
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
