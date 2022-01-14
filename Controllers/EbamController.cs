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
    public class EbamController : Controller
    {
        private ebam_app_dbEntities db = new ebam_app_dbEntities();
        // GET: Ebam
        public ActionResult Index()
        {
           /// var ebamApps = db.EbamApps.Include(e => e.AppName);
            //var ebamApps2 = new SelectList(db.AppManagers, "Id", "Manager");
           // var ebamApps2 = new SelectList(db.AppManagers, "Id", "Manager");
           //var grouping =  ebamApps.GroupBy(a => a.SupportId,a=>a.AppManager);
            ///ViewBag.TotalStudents = ebamApps.Count();
           // ViewBag.TotalMan = ebamApps2.Count();
            //ViewBag.TotalMan2 = grouping.Count();
            return View();
        }
    }
}