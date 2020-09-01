using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InlaksAlumniWebsite.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Donation Title")]
        public string DonationTitle { get; set; }

        [Range(1, 100000)]
        [DataType(DataType.Currency)]
        public float Amount { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Donation Type")]
        public string DonationType { get; set; }

        public string Email { get; set; }
        public int AlumniId { get; set; }
        public Alumni Alumni { get; set; }
    }
}