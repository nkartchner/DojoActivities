using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CBelt.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name cannot be left blank.")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters in length.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "First Name")]
        public string firstName { get; set; }


        [Required(ErrorMessage = "Last name cannot be left blank.")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters in length.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }


        [Required(ErrorMessage = "Email cannot be left blank.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }


        [Required(ErrorMessage = "Password cannot be left blank.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=^.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;&#39;?/&gt;.&lt;,])(?!.*\s).*$", ErrorMessage = "Password must be at least 8 characters and include 1 lowercase letter, 1 uppercase letter, 1 number, and 1 special character.")]
        [Display(Name = "Password")]
        public string password { get; set; }


        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;


        [Required]
        [NotMapped]
        [Compare("password")]
        [DataType(DataType.Password)]
        public string confirm { get; set; }



        public List<Participant> Participating { get; set; }




    }
}
