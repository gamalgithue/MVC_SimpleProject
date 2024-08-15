using AutoMapper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using FirstPro.DAL.Database;
using FirstPro.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using FirstPro.BLL.Helper;

namespace FirstPro.Web.Controllers
{

    [Authorize(Roles ="Admin,HR")]
    public class EmployeeController : Controller
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
        public async Task<IActionResult> Index()
        {
            var emps = await employee.getEmployeesAsync(1, 15);
            return View(emps);

        }
        public async Task<IActionResult> Details(int id)
        {

            var result = await employee.getEmployeeAsync(id);

            return View(result);
        }
        [HttpGet,Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name");
            ViewBag.CountryList = new SelectList(await country.getCountriesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(EmployeeDTO employeevm)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    //employeevm.CvName = FileUploader.Upload("Cvs", employeevm.Cv);
                    //employeevm.PhotoName = FileUploader.Upload("Photos", employeevm.Photo);

                    await employee.CreateOrUpdateEmployeeAsync(employeevm);
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name");
            ViewBag.CountryList = new SelectList(await country.getCountriesAsync(), "Id", "Name");



            return View(employeevm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var emp = await employee.getEmployeeAsync(id);
            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name", emp.DepartmentId);
            ViewBag.CountryList = new SelectList(await country.getCountriesAsync(), "Id", "Name", emp.District.City.Country.Id);
            ViewBag.CityList = new SelectList(await city.getCitiesAsync(), "Id", "Name", emp.District.City.Id);
            ViewBag.DistrictList = new SelectList(await district.getDistrictsAsync(), "Id", "Name", emp.DistrictId);



            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeDTO employeevm)
        {


            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name", employeevm.DepartmentId);
            ViewBag.CountryList = new SelectList(await country.getCountriesAsync(), "Id", "Name", employeevm.CountryName);

            try
            {
                if (ModelState.IsValid)
                {

                    await employee.CreateOrUpdateEmployeeAsync(employeevm);



                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

            }

            return View(employeevm);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {


            var result = await employee.getEmployeeAsync(id);

            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name", result.DepartmentId);
            //ViewBag.DistrictList = new SelectList(await country.getCountriesAsync(), "Id", "Name",result.District.City.Country.Id);






            return View(result);
        }
        [HttpPost]

        //[ActionName("Delete")]
        public async Task<IActionResult> Delete(EmployeeDTO employeevm)
        {

            await employee.DeleteEmployeeAsync(employeevm);
            //employeevm.CvName = FileUploader.RemoveFile("Cvs", employeevm.Cv);
            //employeevm.PhotoName = FileUploader.RemoveFile("Photos", employeevm.Photo);

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<JsonResult> GetCitiesByCntryId(int cntryid)
        {
            var result = await city.getCitiesAsync(cntryid);
            return Json(result);
        }
        [HttpPost]

        public async Task<JsonResult> GetDistrictsByCityId(int cityid)
        {
            var result = await district.getDistrictsAsync(cityid);
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> Paging(int pageNumber, int pageSize)
        {
            var emps = await employee.getEmployeesAsync(pageNumber, pageSize);
            return Json(new { data = emps });

        }
        [HttpPost]
        //public async Task<JsonResult> GetAllData()
        //{

        //    var draw = Request.Form["draw"];
        //    var start = Convert.ToInt32(Request.Form["start"]);
        //    var length = Convert.ToInt32(Request.Form["length"]);

        //    IQueryable<Employee> Employees = context.Employees;
        //    var recordsTotal = Employees.Count();

        //    var cdata = await Employees.Skip(start).Take(length).ToListAsync();
        //    var data = mapper.Map<EmployeeDTO>(cdata);
        //    var jsonData = new { recordsFiltered = recordsTotal, data };
        //    return new JsonResult(jsonData);
        //}

        [HttpPost]
        public async Task<IActionResult> GetData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"].FirstOrDefault());
                var sortColumnName = Request.Form["columns[" + sortColumnIndex + "][name]"].FirstOrDefault();
                var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;

                var data = (from Employee in context.Employees
                                select new EmployeeDTO
                            {



                                Id = Employee.Id,
                                Name = Employee.Name,
                                Salary = Employee.Salary,
                                Email = Employee.Email,
                                HireDate = Employee.HireDate,
                                DepartmentName = Employee.Department.Name,
                                DistrictName = Employee.District.Name,
                                CityName = Employee.District.City.Name,
                                CountryName = Employee.District.City.Country.Name
                                //CvName=Employee.CvName,
                                // PhotoName=Employee.PhotoName
                                
                            });



                // Sorting
                if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    switch (sortColumnName)
                    {
                        case "Id":
                            data = sortColumnDir == "asc" ? data.OrderBy(e => e.Id) : data.OrderByDescending(e => e.Id);
                            break;
                        case "Name":
                            data = sortColumnDir == "asc" ? data.OrderBy(e => e.Name) : data.OrderByDescending(e => e.Name);
                            break;
                        case "Salary":
                            data = sortColumnDir == "asc" ? data.OrderBy(e => e.Salary) : data.OrderByDescending(e => e.Salary);
                            break;

                        case "Email":
                            data = sortColumnDir == "asc" ? data.OrderBy(e => e.Email) : data.OrderByDescending(e => e.Email);
                            break;

                        case "HireDate":
                            data = sortColumnDir == "asc" ? data.OrderBy(e => e.HireDate) : data.OrderByDescending(e => e.HireDate);
                            break;
                        case "DepartmentName":
                            data = sortColumnDir == "asc" ? data.OrderBy(e => e.DepartmentName) : data.OrderByDescending(e => e.DepartmentName);
                            break;
                        case "DistrictName":
                            data = sortColumnDir == "asc" ? data.OrderBy(e => e.DistrictName) : data.OrderByDescending(e => e.DistrictName);
                            break;
                        case "CityName":
                            data = sortColumnDir == "asc" ? data.OrderBy(e => e.CityName) : data.OrderByDescending(e => e.CityName);
                            break;
                        case "CountryName":
                            data = sortColumnDir == "asc" ? data.OrderBy(e => e.CountryName) : data.OrderByDescending(e => e.CountryName);
                            break;
                        default:
                            break;
                    }
                }
                // Filtering
                if (!string.IsNullOrEmpty(searchValue))
                {
                    data = data.Where(e =>
                        e.Name.Contains(searchValue) ||
                        e.Email.Contains(searchValue) ||
                        e.DepartmentName.Contains(searchValue) ||
                        e.DistrictName.Contains(searchValue) ||
                        e.CityName.Contains(searchValue) ||
                        e.CountryName.Contains(searchValue) ||
                        (e.Salary!=null&& ((decimal)e.Salary).ToString().Contains(searchValue)) // Include search by Salary
                        );

                }

                var totalRecords = await data.CountAsync();
                var cData = await data.Skip(skip).Take(pageSize).ToListAsync();

                var jsonData = new
                {
                    draw = draw,
                    recordsFiltered = totalRecords,
                    recordsTotal = totalRecords,
                    data = cData
                };

                return new JsonResult(jsonData);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while processing the request: {ex}");

                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }




    }

