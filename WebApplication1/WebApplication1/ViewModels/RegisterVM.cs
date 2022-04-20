using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModels
{
    public class RegisterVM
    {
        [Required, MinLength(5)]
        public string Username { get; set; }

        [MinLength(5), Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password"), DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        public string ComfirmPassword { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

    }
}