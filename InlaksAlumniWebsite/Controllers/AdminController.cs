using InlaksAlumniWebsite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

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
            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var usr = db.Admins.SingleOrDefault (u => u.Email == user.Email && u.Password == user.Password);

                if (usr != null)
                {
                    Session["User"] = usr;
                    return RedirectToAction("Inbox");
                }
                else
                {
                    ViewBag.Message = "Invalid Email or Password";
                    //ModelState.AddModelError("", "Username or Password is Invalid");
                }
            }
            return View();
        }




        //GET
        public ActionResult CreateAdmin()
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        [HttpPost]
        public ActionResult CreateAdmin(Admin admin)
        {
            if(ModelState != null)
            {
                using (InlaksAlumniContext db = new InlaksAlumniContext())
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        [HttpPost]
        public ActionResult CreateAlumni(Alumni alumni)
        {
            if (ModelState != null)
            {
                using (InlaksAlumniContext db = new InlaksAlumniContext())
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateDonation(Donation donation)
        {
            if (ModelState != null)
            {
                using (InlaksAlumniContext db = new InlaksAlumniContext())
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        [HttpPost]
        public ActionResult CreateEvent(Event e)
        {
            if (ModelState != null)
            {
                using (InlaksAlumniContext db = new InlaksAlumniContext())
                {
                    db.Events.Add(e);
                    db.SaveChanges();
                }
                ModelState.Clear();

                ViewBag.Title = "Successfully Added";
            }
            return View();
        }



        public ActionResult Inbox(int? page)
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            Admin admin = Session["User"] as Admin;
            if (admin == null)
            {
                return HttpNotFound();
            }
            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var feed = db.Feedbacks.ToList().ToPagedList(page ?? 1, 8);
                if (feed == null)
                {
                    return RedirectToAction("Inbox");
                }
                else
                {
                    feed.Reverse();
                }
                return View(feed);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Login");
        }


        //List of registered Administrators
        public ActionResult UpdateAdmin(int? page)
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var user = db.Admins.ToList().ToPagedList(page ?? 1, 5);
                if (user == null)
                {
                    return RedirectToAction("UpdateAdmin");
                }
                user.Reverse();

                return View(user);
            }

        }

        public ActionResult UpdateAlumni(int? page)
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var user = db.Alumnis.ToList().ToPagedList(page?? 1, 5);
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult ManageEvent(int ? page)
        {
            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var user = db.Events.ToList().ToPagedList(page ?? 1, 5);
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UploadPhoto(EventImage e, HttpPostedFileBase UploadImage)
        {

            if (ModelState.IsValid)
            {
                using (InlaksAlumniContext db = new InlaksAlumniContext())
                {
                    try
                    {
                        if (UploadImage != null)
                        {
                            string path = Path.Combine(Server.MapPath("~/Content/photos"), Path.GetFileName(UploadImage.FileName));
                            UploadImage.SaveAs(path);
                            ViewBag.FileStatus = "File uploaded successfully.";
                        }

                        else{
                            return View();
                        }
                    }
                    catch (Exception)
                    {

                        ViewBag.FileStatus = "Error while file uploading.";
                    }
                    e.ImagesUrl = UploadImage.FileName;

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
            else
            {
                return View();
            }

        }


        public ActionResult ViewCollections()
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
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

        public ActionResult ViewGallery(int? page)
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var user = db.EventImages.ToList().ToPagedList(page ?? 1, 4);
                if (user == null)
                {
                    return RedirectToAction("UploadPhoto");
                }
                user.Reverse();
                return View(user);
            }
        }

        //GET
        public ActionResult Edit(int? id)
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var alumni = db.Alumnis.Where(row => row.AlumniId == id).FirstOrDefault();
                return View(alumni);
            }
        }


        [HttpPost]
        public ActionResult Edit(Alumni alumni)
        {
            using (InlaksAlumniContext db = new InlaksAlumniContext())
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
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

            using (InlaksAlumniContext db = new InlaksAlumniContext())
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var admin = db.Admins.Where(row => row.AdminId == id).FirstOrDefault();
                return View(admin);
            }
        }

        [HttpPost]
        public ActionResult EditAdmin(Admin admin)
        {
            using (InlaksAlumniContext db = new InlaksAlumniContext())
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
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

            using (InlaksAlumniContext db = new InlaksAlumniContext())
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var evt = db.Events.Where(row => row.EventId == id).FirstOrDefault();
                return View(evt);
            }
        }

        [HttpPost]
        public ActionResult EditEvent(Event e)
        {
            using (InlaksAlumniContext db = new InlaksAlumniContext())
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
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
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

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var deleteEvnt = db.Events.Where(row => row.EventId == id).FirstOrDefault();
                db.Events.Remove(deleteEvnt);
                db.SaveChanges();
                return RedirectToAction("ManageEvent");
            }
        }



        public ActionResult ManageAccount(Admin admin)
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (admin.Password != admin.ConfirmPassword)
            {
                return RedirectToAction("UpdateAccount");

            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var user = db.Admins.SingleOrDefault(c => c.Username == admin.Username);
                if (user == null)
                {
                    return View();
                }

                user.Password = admin.Password;
                user.ConfirmPassword = admin.ConfirmPassword;
                ViewBag.Message = "Successful";
                db.SaveChanges();

            }
            return View();
        }


        public ActionResult DeleteImage(int? id)
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var evt = db.EventImages.Where(row => row.ImageId == id).FirstOrDefault();
                return View(evt);
            }
        }

        [HttpPost]
        [ActionName("DeleteImage")]
        public ActionResult ConfirmDeleteImage(int? id)
        {

            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var deleteImg = db.EventImages.Where(row => row.ImageId == id).FirstOrDefault();
                db.EventImages.Remove(deleteImg);
                db.SaveChanges();
                return RedirectToAction("ViewGallery");
            }
        }

        public ActionResult DeleteMessage(int? id)
        {
            if (!this.AuthenticateUser())
            {
                return RedirectToAction("Login");
            }

            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var msg = db.Feedbacks.Where(row => row.Id == id).FirstOrDefault();
                return View(msg);
            }
        }

        [HttpPost]
        [ActionName("DeleteMessage")]
        public ActionResult ConfirmDeleteMessage(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            using (InlaksAlumniContext db = new InlaksAlumniContext())
            {
                var deleteMsg = db.Feedbacks.Where(row => row.Id == id).FirstOrDefault();
                db.Feedbacks.Remove(deleteMsg);
                db.SaveChanges();
                return RedirectToAction("Inbox");
            }
        }


        public Boolean AuthenticateUser()
        {
            if (Session["User"] != null)
            {
                return true;
            }

            return false;
        }
    }
}