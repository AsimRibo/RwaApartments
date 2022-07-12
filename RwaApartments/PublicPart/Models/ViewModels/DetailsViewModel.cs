using RwaUtilities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicPart.Models.ViewModels
{
    public class DetailsViewModel
    {
        public Apartment Apartment { get; set; }

        public IList<ApartmentReview> Reviews { get; set; }

        public string IdUser { get; set; } = "0";

        [Required(ErrorMessage = "Name can't be empty")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email can't be empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone can't be empty")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Must specify")]
        public int Adults { get; set; }

        [Required(ErrorMessage = "Must specify")]
        public int Children { get; set; }


    }
}