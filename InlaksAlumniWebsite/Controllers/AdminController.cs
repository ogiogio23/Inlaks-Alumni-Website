using InlaksAlumniWebsite.Models;
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


        //GET
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin user)
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var usr = db.Admins.Single(u => u.Email == user.Email && u.Password == user.Password);

                if (usr != null)
                {
                    Session["UserId"] = usr.Email.ToString();
                    Session["Username"] = usr.Username.ToString();

                    return RedirectToAction("Inbox");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is Invalid");
                }
            }
            return View();
        }




        //GET
        public ActionResult CreateAdmin()
        {
            ViewBag.Title = "CreateAdmin";
            return View();
        }


        [HttpPost]
        public ActionResult CreateAdmin(Admin admin)
        {
            if(ModelState != null)
            {
                using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
                {
                    var result = db.Admins.Where(a => a.Email == admin.Email).FirstOrDefault();
                    if (result != null)
                    {

                        ViewBag.Feedback = "Record Already Exist.";
                        return RedirectToAction("CreateAdmin");
                    }
                    db.SaveChanges();


                    db.Admins.Add(admin);
                    db.SaveChanges();
                }

                ModelState.Clear();

                //Display notification to users
                ViewBag.Message = admin.Username + " " + "Successfully Registered.";
            }

            return View();
        }


        public ActionResult CreateAlumni()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateAlumni(Alumni alumni)
        {
            if (ModelState != null)
            {
                using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
                {
                    var result = db.Alumnis.Where(a => a.Email == alumni.Email).FirstOrDefault();
                    if (result != null)
                    {

                        ViewBag.Feedback = "Record Already Exist.";
                        return RedirectToAction("CreateAlumni");
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


        //GET
        public ActionResult CreateDonation()
        {
            ViewBag.Title = "CreateDonation";
            return View();
        }

        [HttpPost]
        public ActionResult CreateDonation(Donation donation)
        {
            if (ModelState != null)
            {
                using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
                {
                    var result1 = db.Donations.Where(a => a.Email == donation.Email).FirstOrDefault();
                    if (result1 != null)
                    {

                        ViewBag.Feedback = "Record Already Exist.";
                        return RedirectToAction("");
                    }
                    db.SaveChanges();


                    var result = db.Alumnis.Single(a => a.Email == donation.Email);
                    db.SaveChanges();

                    if (result == null)
                    {
                        return View();

                    }


                    donation.AlumniId = result.AlumniId;
                    db.Donations.Add(donation);

                    db.SaveChanges();

                    ModelState.Clear();

                    ViewBag.Feedback = "Successfully Registered.";
                }
            }
            return View();
        }


        //GET
        public ActionResult CreateEvent()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateEvent(Event e)
        {
            if (ModelState != null)
            {
                using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
                {
                    db.Events.Add(e);
                    db.SaveChanges();
                }
                ModelState.Clear();

                ViewBag.Title = "Successfully Added";
            }
            return View();
        }



        public ActionResult Inbox()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Login");
        }


        //List of registered Administrators
        public ActionResult UpdateAdmin()
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var user = db.Admins.ToList();
                if (user == null)
                {
                    return RedirectToAction("UpdateAdmin");
                }
                user.Reverse();

                return View(user);
            }

        }

        public ActionResult UpdateAlumni()
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var user = db.Alumnis.ToList();
                if (user == null)
                {
                    return RedirectToAction("UpdateAlumni");
                }
                user.Reverse();
                return View(user);
            }
        }

        public ActionResult UpdateDonation()
        {
            return View();
        }

        public ActionResult ManageEvent()
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
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

        public ActionResult UploadPhoto()
        {
            ViewBag.Title = "UploadPhoto";
            return View();
        }

        [HttpPost]
        public ActionResult UploadPhoto([Bind(Include = "Id, ImageUrl,EventId")] EventImage e, HttpPostedFileBase UploadImage)
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {


            if (ModelState != null)
            {
                    if (UploadImage.ContentType == "image/jpg" || UploadImage.ContentType == "image/jpeg" || UploadImage.ContentType == "image/png")
                    {
                        UploadImage.SaveAs(Server.MapPath("/") + "/Content/images" + UploadImage.FileName);
                        e.ImagesUrl = UploadImage.FileName;
                    }
                    else
                    {
                        return View();
                    }

            }
                else
                {
                    return View();
                }

                var res = db.Events.Single(a => a.EventId == e.EventId);
                db.SaveChanges();


                if (res == null)
                {
                    return View();
                }

                e.EventId = res.EventId;
                db.EventImages.Add(e);

                db.SaveChanges();

                return RedirectToAction("ViewGallery");

            }


        }


        public ActionResult ViewCollections()
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var user = db.Donations.ToList();
                if (user == null)
                {
                    return RedirectToAction("ViewCollections");
                }
                user.Reverse();
                return View(user);
            }
        }

        public ActionResult ViewGallery()
        {
            ViewBag.Title = "ViewGallery";
            return View();
        }

        //GET
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var alumni = db.Alumnis.Where(row => row.AlumniId == id).FirstOrDefault();
                return View(alumni);
            }
        }


        [HttpPost]
        public ActionResult Edit(Alumni alumni)
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var emp1 = db.Alumnis.Where(row => row.AlumniId == alumni.AlumniId).FirstOrDefault();
                if (emp1 == null)
                {
                    return RedirectToAction("UpdateAlumni");
                }

                emp1.FirstName = alumni.FirstName;
                emp1.LastName = alumni.LastName;
                emp1.Email = alumni.Email;
                emp1.PastEmployeeId = alumni.PastEmployeeId;
                emp1.PhoneNumber = alumni.PhoneNumber;
                emp1.DateOfEmployment = alumni.DateOfEmployment;
                emp1.PositionHeld = alumni.PositionHeld;
                emp1.DateLeft = alumni.DateLeft;
                emp1.Gender = alumni.Gender;
                emp1.Status = alumni.Status;

                int res = db.SaveChanges();
                if (res > 0)
                {
                    ViewBag.Message = "Successfully Updated";
                }
            }
            return View();
        }

        public ActionResult DeleteAlumni(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var alumni = db.Alumnis.Where(row => row.AlumniId == id).FirstOrDefault();
                return View(alumni);
            }
        }


        [HttpPost]
        [ActionName("DeleteAlumni")]
        public ActionResult ConfirmDeleteAlumni(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var deleteAlumni = db.Alumnis.Where(row => row.AlumniId == id).FirstOrDefault();
                db.Alumnis.Remove(deleteAlumni);
                db.SaveChanges();
                return RedirectToAction("UpdateAlumni");
            }
        }


        //GET
        public ActionResult EditAdmin(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var admin = db.Admins.Where(row => row.AdminId == id).FirstOrDefault();
                return View(admin);
            }
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin admin)
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var emp1 = db.Admins.Where(row => row.AdminId == admin.AdminId).FirstOrDefault();
                if (emp1 == null)
                {
                    return RedirectToAction("UpdateAdmin");
                }

                emp1.FirstName = admin.FirstName;
                emp1.LastName = admin.LastName;
                emp1.Email = admin.Email;
                emp1.PhoneNumber = admin.PhoneNumber;
                emp1.Gender = admin.Gender;


                int res = db.SaveChanges();
                if (res > 0)
                {
                    ViewBag.Message = "Successfully Updated";
                }
            }
            return View();
        }


        public ActionResult DeleteAdmin(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var admin = db.Admins.Where(row => row.AdminId == id).FirstOrDefault();
                return View(admin);
            }
        }

        [HttpPost]
        [ActionName("DeleteAdmin")]
        public ActionResult ConfirmDeleteAdmin(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var deleteAdmin = db.Admins.Where(row => row.AdminId == id).FirstOrDefault();
                db.Admins.Remove(deleteAdmin);
                db.SaveChanges();
                return RedirectToAction("UpdateAdmin");
            }
        }


        //GET
        public ActionResult EditEvent(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var evt = db.Events.Where(row => row.EventId == id).FirstOrDefault();
                return View(evt);
            }
        }

        [HttpPost]
        public ActionResult EditEvent(Event e)
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var emp1 = db.Events.Where(row => row.EventId == e.EventId).FirstOrDefault();
                if (emp1 == null)
                {
                    return RedirectToAction("ManageEvent");
                }

                emp1.EventTitle = e.EventTitle;
                emp1.EventLocation = e.EventLocation;
                emp1.EventDate = e.EventDate;
                emp1.EventTime = e.EventTime;
                emp1.DateRegistered = e.DateRegistered;


                int res = db.SaveChanges();
                if (res > 0)
                {
                    ViewBag.Message = "Successfully Updated";
                }
            }
            return View();
        }




        public ActionResult DeleteEvent(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var evt = db.Events.Where(row => row.EventId == id).FirstOrDefault();
                return View(evt);
            }
        }

        [HttpPost]
        [ActionName("DeleteEvent")]
        public ActionResult ConfirmDeleteEvent(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var deleteEvnt = db.Events.Where(row => row.EventId == id).FirstOrDefault();
                db.Events.Remove(deleteEvnt);
                db.SaveChanges();
                return RedirectToAction("ManageEvent");
            }
        }



        public ActionResult ManageAccount(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var admin = db.Admins.Where(row => row.AdminId == id).FirstOrDefault();
                return View(admin);
            }
        }

        [HttpPost]
        public ActionResult ManageAccount(Admin admin)
        {
            using (InlaksAlumniDbContext db = new InlaksAlumniDbContext())
            {
                var emp1 = db.Admins.Where(row => row.AdminId == admin.AdminId).FirstOrDefault();
                if (emp1 == null)
                {
                    return RedirectToAction("ManageAccount");
                }

                emp1.Username = admin.Username;
                emp1.Password = admin.Password;
                emp1.ConfirmPassword = admin.ConfirmPassword;



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