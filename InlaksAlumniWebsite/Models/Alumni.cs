using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InlaksAlumniWebsite.Models
{
    public class Alumni
    {
        [Key]
        public int AlumniId { get; set; }

        [Required(ErrorMessage = "First Name is required!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        public string PastEmployeeId { get; set; }

        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public string DateOfEmployment { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string PositionHeld { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public string DateLeft { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        //[RegularExpression(@"^((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,64})$")]
        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Gender { get; set; }

        [SqlDefaultValue(DefaultValue = "getdate()")]
        public DateTime DateRegistered { get; set; } = DateTime.Now;

        public string Status { get; set; } = "PENDING";

        public List<Donation> Donations { get; set; }
    }
}