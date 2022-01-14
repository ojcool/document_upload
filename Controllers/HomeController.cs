using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminLogin.Models;
using static AdminLogin.Models.AuthLogin;
using AdminLogin.LoginServices;

namespace AdminLogin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Login(AuthLogin model, string returnUrl)
        //{
        //    return RedirectToAction("Index", "Ebam");

        //}
        //public ActionResult Login(AuthLogin model, string returnUrl)
        public ActionResult Login()
        {


            return RedirectToAction("Index", "Ebam");
            //var roles = new List<string>();
            //var IsAuthenticate = Authenticate(model.UserName, model.Password);
            //if (IsAuthenticate)
            //{

            //    var email = model.UserName + "@diamondbank.com";
            //    Console.Write("Internal Login", model.UserName, "Login successful.");

            //    return RedirectToAction("Index", "Ebam");
            //}

            //Console.Write("Internal Login", model.UserName, "Login failed => Invalid username or password.");

            //ViewBag.ErrorMessage = "Invalid username or password.";
           // return View();
        }


        public static bool Authenticate(string username, string password)
        {
           
          //  roles = new List<string>();
            username = username.Trim().ToLower();

            var email = (username + "@diamondbank.com").ToLower();
            var auth = false;
            var client = new databasefunctionsSoapClient();

            auth = client.ActiveDirectoryAuthuser(username, password).AuthCode == 1 ? true : false;
                 
            return auth;
        }

        public ActionResult LogOff()
        {
            //ViewBag.Message = "Your contact page.";
            return RedirectToAction("Index", "Home");
          
            // return View();
        }


        

            



    }
}