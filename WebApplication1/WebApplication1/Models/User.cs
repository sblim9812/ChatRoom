  using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(5)]
        public string Username { get; set; }

        [MinLength(5),Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name ="Picture")]
        public string ImageUrl { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? CreatedOn { get; set; }


    }
}