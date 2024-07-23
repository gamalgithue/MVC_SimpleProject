using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Modals
{
   public class ForgetPasswordDTO
    {
        [Required(ErrorMessage = "email required")]
        [EmailAddress(ErrorMessage = "invalid email address")]
        public string Email { get; set; }

    }
}
