using FirstPro.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Modals
{
   public class EmployeeDTO
    {

        [Key]
        public int Id { get; set; }

        [Required,StringLength(50)]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Salary Is Required")]
        [Range(6000,20000,ErrorMessage ="Range Salary between 6000 and 20000")]
        public decimal Salary { get; set; }

        [Required]
        public DateTime HireDate { get; set; }



        [EmailAddress(ErrorMessage ="Please Enter Your Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address.")]



        public string? Email { get; set; }
        public bool IsActive { get; set; }


        [Required(ErrorMessage = "Date Is Required")]
        public DateTime CreationDate { get; set; }
        
        public string? PhotoName { get; set; }
        public string? CvName { get; set; }

         public IFormFile Photo { get; set; }
        public IFormFile Cv { get; set; }


        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int DistrictId { get; set; }

        //[Required(ErrorMessage ="District is Required !")]
        public District? District { get; set; }
        public string? DistrictName { get; set; }

        public string? CityName { get; set; }
        public string? CountryName { get; set; }


        public string? DepartmentName { get; set; }
        public string? DepartmentCode { get; set; }


        public EmployeeDTO()
        {
            CreationDate = DateTime.Now;
            IsActive = true;
        }


    }
}
