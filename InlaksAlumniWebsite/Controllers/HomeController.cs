using InlaksAlumniWebsite.Models;
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

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Feedback feedback)
        {
            if (ModelState != null)
            {
                using (InlaksAlumniContext db = new InlaksAlumniContext())
                {
                    db.Feedbacks.Add(feedback);
                    db.SaveChanges();
                }

                ModelState.Clear();

                //Display notification to users
                ViewBag.Message = "Message Sent.";
            }
            return View();
        }

        public ActionResult Event()
        {
            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var user = db.Events.ToList();
                if (user == null)
                {
                    return RedirectToAction("ManageEvent");
                }
                user.Reverse();

                return View(user);
            }
        }

        public ActionResult Gallery()
        {
            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var user = db.EventImages.ToList();
                if (user == null)
                {
                    return RedirectToAction("Gallery");
                }
                user.Reverse();

                return View(user);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Alumni alumni)
        {
            ViewBag.Message = "";
            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var usr = db.Alumnis.SingleOrDefault(u => u.Email == alumni.Email && u.Password == alumni.Password);

                if (usr != null && usr.Status=="APPROVED")
                {
                    Session["User"] = usr;
                    return RedirectToAction("EmployeeProfile");
                }
                else
                {
                    ViewBag.Message = "Invalid username or password.";
                    //ModelState.AddModelError("", "Username or Password is Invalid");
                }
            }
            return View();

        }


        public ActionResult Logout()
        {

            Session.Clear();

            return RedirectToAction("Login");
        }




        public ActionResult Register()
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(Alumni alumni)
        {
            if (ModelState != null)
            {
                using (InlaksAlumniContext db = new InlaksAlumniContext())
                {
                    var result = db.Alumnis.Where(a => a.Email == alumni.Email).FirstOrDefault();
                    if (result != null)
                    {

                        ViewBag.Feedback = "Record Already Exist.";
                        return RedirectToAction("Register");
                    }
                    db.SaveChanges();


                    db.Alumnis.Add(alumni);
                    db.SaveChanges();
                }

                ModelState.Clear();

                //Display notification to users
                ViewBag.Message = alumni.Username + " " + "Successfully Registered.";
            }
            return View();
        }

        public ActionResult Payment()
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult AlumniProfile()
        {
            ViewBag.Title = "New Alumni.";

            return View();
        }



        public ActionResult UpdateAccount(Alumni alumni)
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (alumni.Password != alumni.ConfirmPassword)
            {
                return RedirectToAction("UpdateAccount");

            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var user = db.Alumnis.SingleOrDefault(c => c.Username == alumni.Username);
                if (user == null)
                {
                    return View();
                }

                user.Password = alumni.Password;
                user.ConfirmPassword = alumni.ConfirmPassword;
                ViewBag.Message = "Successful";
                db.SaveChanges();

            }
            return View();
        }




        public ActionResult UploadEvidence()
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        public Boolean AuthenticateUser()
        {
            if (Session["User"] != null)
            {
                return true;
            }

            return false;
        }

        public ActionResult EmployeeProfile()
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            Alumni alumni = Session["User"] as Alumni;
             if (alumni == null)
             {
                 return HttpNotFound();
             }
             using (InlaksAlumniContext db = new InlaksAlumniContext())
             {
                 var alumniInfo = db.Alumnis.SingleOrDefault(row => row.AlumniId == alumni.AlumniId);
                 return View(alumniInfo);
             }
        }


        public ActionResult EditProfile()
        {

            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        [HttpPost]
        public ActionResult EditProfile(Alumni alumni)
        {
            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var emp1 = db.Alumnis.Where(row => row.AlumniId == alumni.AlumniId).FirstOrDefault();
                if (emp1 == null)
                {
                    return RedirectToAction("EmployeeProfile");
                }

                emp1.FirstName = alumni.FirstName;
                emp1.LastName = alumni.LastName;
                emp1.Email = alumni.Email;
                emp1.PastEmployeeId = alumni.PastEmployeeId;
                emp1.PhoneNumber = alumni.PhoneNumber;
                emp1.DateOfEmployment = alumni.DateOfEmployment;
                emp1.PositionHeld = alumni.PositionHeld;
                emp1.Username = alumni.Username;
                emp1.Gender = alumni.Gender;



                int res = db.SaveChanges();
                if (res > 0)
                {
                    ViewBag.Message = "Successfully Updated";
                }
            }
            return View();
        }
    }
}
