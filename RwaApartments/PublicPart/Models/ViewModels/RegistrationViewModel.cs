using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicPart.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Can't be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Can't be empty")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Can't be empty")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}