using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.DAL.Entities
{

    [Table("Employees")]
    public class Employee
    {

        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; } /*= DateTime.Now;*/
        public string? Email { get; set; }
        public bool IsActive { get; set; }/*= true;*/

        public DateTime CreationDate { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

       



    }
}
