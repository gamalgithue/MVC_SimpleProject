using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Modals
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Department Name is Required")]
        [MinLength(2,ErrorMessage ="Min Length is 2")]
        [MaxLength(50,ErrorMessage ="Max Length is 50")]

        public string Name { get; set; }
        [Required(ErrorMessage ="Code Of Department is Required")]
        public string Code { get; set; }
        [Required,Range(50,70,ErrorMessage ="No Of Emp in Dep must be between 50 and 70")]
        public int NoOfEmp { get; set; }
    }
}
