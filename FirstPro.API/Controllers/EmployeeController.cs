using AutoMapper;
using FirstPro.BLL.Helper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using FirstPro.DAL.Database;
using FirstPro.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstPro.API.Controllers
{
    public class EmployeeController : BaseController
    {
        #region prop
        private readonly ApplicationDbContext context;
        private readonly IEmployeeService employee;
        private readonly IMapper mapper;
        private readonly IDepartmentService department;
        private readonly ICountryService country;
        private readonly ICityService city;
        private readonly IDistrictService district;
        #endregion


        #region ctor
        public EmployeeController(ApplicationDbContext _context, IEmployeeService _employee, IMapper _mapper, IDepartmentService department, ICountryService _country, ICityService _city, IDistrictService _district)
        {
            this.context = _context;
            this.employee = _employee;
            this.mapper = _mapper;
            this.department = department;
            this.country = _country;
            this.city = _city;
            this.district = _district;
        }
        #endregion

        #region Actions

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var emps = await employee.getEmployeesAsync(1, 15);

                return Ok(new ApiResponse<IEnumerable<EmployeeDTO>>
                {
                    Code = 200,
                    Status = "Ok",
                    Result = emps
                });
            }

            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>
                {
                    Code = 404,
                    Status = "Not Found",
                    Result = ex.Message
                });



            }
        }


        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> Details(int id)
        {

            try
            {
                var result = await employee.getEmployeeAsync(id);
                return Ok(new ApiResponse<EmployeeDTO>
                {
                    Code = 200,
                    Status = "Ok",
                    Result = result
                });

            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>
                {
                    Code = 404,
                    Status = "Not Found",
                    Result = ex.Message
                });



            }


        }



        //[HttpPost("CreateEmployee")]
        //public async Task<IActionResult> Create(EmployeeDTO employeevm)
        //{

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {


        //            await employee.CreateOrUpdateEmployeeAsync(employeevm);
        //            //ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name");
        //            //ViewBag.CountryList = new SelectList(await country.getCountriesAsync(), "Id", "Name");
        //            return Ok(new ApiResponse<string>
        //            {
        //                Code = 200,
        //                Status = "Ok",
        //                Result = "Employee Created Successfully"
        //            });

        //        }
        //        else
        //        {
        //            return BadRequest(new ApiResponse<string>
        //            {
        //                Code = 400,
        //                Status = "Bad Request",
        //                Result = "Invalid Employee Data"
        //            });

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(new ApiResponse<string>
        //        {
        //            Code = 404,
        //            Status = "Not Found",
        //            Result = ex.Message
        //        });
        //    }


        //}


    }

    #endregion
}
    
