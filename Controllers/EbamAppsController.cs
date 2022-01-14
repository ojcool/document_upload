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
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Web.UI;

namespace AdminLogin.Controllers
{
    public class EbamAppsController : Controller
    {
        private ebam_app_dbEntities db = new ebam_app_dbEntities();

        // GET: EbamApps
        public ActionResult Index(string sortOrder, string currentFilter,string searchString, int? page, int? i, int? selectedName)
        {
            var ebamApps = db.EbamApps.Include(e => e.AppManager);  ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "app_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var leaves = from l in db.EbamApps select l;
            if (!String.IsNullOrEmpty(searchString))
            {
                leaves = leaves.Where((x => x.AppName.StartsWith(searchString) || x.Host_IP.StartsWith(searchString) || x.AppManager.Manager.StartsWith(searchString) || searchString == null));
            }
           
            switch (sortOrder)
            {
                case "name_desc":
                    leaves = leaves.OrderByDescending(l => l.AppName);
                    break;
                case "app_desc":
                    leaves = leaves.OrderByDescending(l => l.AppName);
                    break;
                default:
                    leaves = leaves.OrderBy(l => l.AppManager.Manager);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(leaves.ToPagedList(pageNumber, pageSize));
        }

        // GET: EbamApps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EbamApp ebamApp = db.EbamApps.Find(id);
            if (ebamApp == null)
            {
                return HttpNotFound();
            }
            return View(ebamApp);
        }

        // GET: EbamApps/Create
        public ActionResult Create()
        {
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager");
            return View();
        }

        // POST: EbamApps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EbamApp ebamApp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<EbamSupportFile> fileDetails = new List<EbamSupportFile>();
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            EbamSupportFile fileDetail = new EbamSupportFile()
                            {
                                FileName = fileName,
                                Extension = Path.GetExtension(fileName),
                            };
                            fileDetails.Add(fileDetail);

                            var path = Path.Combine(Server.MapPath("~/uploads/"), fileDetail.FileName + fileDetail.Extension);
                            file.SaveAs(path);
                        }
                    }
                    ebamApp.EbamSupportFiles = fileDetails;
                    db.EbamApps.Add(ebamApp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamApp.SupportId);
            return View(ebamApp);
        }

        // GET: EbamApps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EbamApp ebamApp = db.EbamApps.Find(id);
            if (ebamApp == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamApp.SupportId);
            return View(ebamApp);
        }

        // POST: EbamApps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EbamApp ebamApp)
        {
            if (ModelState.IsValid)
            {

                //New Files
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        EbamSupportFile fileDetail = new EbamSupportFile()
                        {
                            FileName = fileName,
                            Extension = Path.GetExtension(fileName),
                            // id = Guid.NewGuid(),
                            FileId = ebamApp.Id
                        };
                        var path = Path.Combine(Server.MapPath("~/uploads/"), fileDetail.FileName + fileDetail.Extension);
                        file.SaveAs(path);

                        db.Entry(fileDetail).State = EntityState.Added;
                    }
                }

                db.Entry(ebamApp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamApp.SupportId);
            return View(ebamApp);
        }

        // GET: EbamApps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EbamApp ebamApp = db.EbamApps.Find(id);
            if (ebamApp == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupportId = new SelectList(db.AppManagers, "Id", "Manager", ebamApp.SupportId);
            return View(ebamApp);
        }

        // POST: EbamApps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EbamApp ebamApp = db.EbamApps.Find(id);
            db.EbamApps.Remove(ebamApp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public FileResult Download(String p, String d)
        {
            return File(Path.Combine(Server.MapPath("~/uploads/"), p), System.Net.Mime.MediaTypeNames.Application.Octet, d);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public void ExportClientsListToCSV()
        {
            List<EbamApp> applists = db.EbamApps.ToList();
            StringWriter sw = new StringWriter();

            sw.WriteLine("\"First Name\",\"Last Name\",\"DOB\",\"Email\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Exported_Users.csv");
            Response.ContentType = "text/csv";

            foreach (var line in applists)
            {
                sw.WriteLine(string.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"",
                                           line.AppName,
                                           line.DB_IP,
                                           line.DB_name,
                                           line.DB_user));
            }

            Response.Write(sw.ToString());

            Response.End();

        }
        public void ExportClientsListToExcel()
        {
            List<EbamApp> applists = db.EbamApps.ToList();
            var grid = new System.Web.UI.WebControls.GridView();

            grid.DataSource = /*from d in dbContext.diners
                              where d.user_diners.All(m => m.user_id == userID) && d.active == true */
                              from d in applists
                              select new
                              {
                                  Application = d.AppName,
                                  HostIP = d.Host_IP,
                                  PublicIP=d.Public_IP,
                                  DB_Name = d.DB_name,
                                  AppManager = d.AppManager.Manager,
                              

                              };

            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Listofapps.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Write(sw.ToString());

            Response.End();

        }


    }
}
