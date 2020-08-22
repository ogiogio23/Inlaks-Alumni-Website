using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InlaksAlumniWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About Us.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Your contact page.";

            return View();
        }

        public ActionResult Event()
        {
            ViewBag.Title = "Event";

            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.Title = "Gallery.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login.";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Title = "New Alumni.";

            return View();
        }

        public ActionResult Payment()
        {
            ViewBag.Title = "New Alumni.";

            return View();
        }

        public ActionResult AlumniProfile()
        {
            ViewBag.Title = "New Alumni.";

            return View();
        }

        public ActionResult UpdateAccount()
        {
            ViewBag.Title = "New Alumni.";

            return View();
        }

        public ActionResult UploadEvidence()
        {
            ViewBag.Title = "New Alumni.";

            return View();
        }
    }
}
