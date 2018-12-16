using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CBelt.Models
{
    public class Login
    {


        [NotMapped]
        [Required(ErrorMessage = "Email cannot be left blank.")]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Password cannot be left blank.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }


    }
}
