using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InlaksAlumniWebsite.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Title = "Dashboard";
            return View();

        }

        public ActionResult CreateAdmin()
        {
            ViewBag.Title = "CreateAdmin";
            return View();
        }

        public ActionResult CreateAlumni()
        {
            ViewBag.Title = "CreateAlumni";
            return View();
        }

        public ActionResult CreateDonation()
        {
            ViewBag.Title = "CreateDonation";
            return View();
        }

        public ActionResult CreateEvent()
        {
            ViewBag.Title = "CreateEvent";
            return View();
        }

        public ActionResult Inbox()
        {
            ViewBag.Title = "Inbox";
            return View();
        }

        public ActionResult UpdateAdmin()
        {
            ViewBag.Title = "UpdateAdmin";
            return View();
        }

        public ActionResult UpdateAlumni()
        {
            ViewBag.Title = "UpdateAlumni";
            return View();
        }

        public ActionResult UpdateDonation()
        {
            ViewBag.Title = "UpdateDonation";
            return View();
        }

        public ActionResult ManageEvent()
        {
            ViewBag.Title = "UpdateEvent";
            return View();
        }

        public ActionResult UploadPhoto()
        {
            ViewBag.Title = "UploadPhoto";
            return View();
        }

        public ActionResult ViewCollections()
        {
            ViewBag.Title = "ViewCollections";
            return View();
        }

        public ActionResult ViewGallery()
        {
            ViewBag.Title = "ViewGallery";
            return View();
        }
    }
}