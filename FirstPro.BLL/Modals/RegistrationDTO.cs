using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Modals
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "full name required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "email required")]
        [EmailAddress(ErrorMessage = "invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password required")]
        [MinLength(6, ErrorMessage = "min len 6")]
        [MaxLength(10, ErrorMessage = "max len 10")]
        public string Password { get; set; }

        [Required(ErrorMessage = "confirm password required")]
        [MinLength(6, ErrorMessage = "min len 6")]
        [MaxLength(10, ErrorMessage = "max len 10")]
        [Compare("Password", ErrorMessage = "password not match")]
        public string ConfirmPassword { get; set; }

        public bool IsAgree { get; set; }
    }
}
