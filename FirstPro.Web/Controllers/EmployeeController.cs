using AutoMapper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using FirstPro.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstPro.Web.Controllers
{
    public class EmployeeController : Controller
    {

        #region prop
        private readonly IEmployeeService employee;
        private readonly IMapper mapper;
        private readonly IDepartmentService department;
        private readonly ICountryService country;
        private readonly ICityService city;
        private readonly IDistrictService district;
        #endregion


        #region ctor
        public EmployeeController(IEmployeeService _employee,IMapper _mapper,IDepartmentService department,ICountryService _country,ICityService _city,IDistrictService _district)
        {
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
            var emps = await employee.getEmployeesAsync(2, 11);
            return View(emps);

        }
        public async Task<IActionResult> Details(int id)
        {

            var result = await employee.getEmployeeAsync(id);

                return View(result);
        }
        [HttpGet]
        public async Task<IActionResult>  Create()
        {
            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name");
            ViewBag.CountryList = new SelectList(await country.getCountriesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO employeevm)
        {

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
            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name");
            ViewBag.CountryList = new SelectList(await country.getCountriesAsync(), "Id", "Name");



            return View(employeevm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            
            var emp = await employee.getEmployeeAsync(id);
            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name",emp.DepartmentId);
            ViewBag.CountryList = new SelectList(await country.getCountriesAsync(), "Id", "Name",emp.District.City.Country.Id);
            ViewBag.CityList = new SelectList(await city.getCitiesAsync(), "Id", "Name", emp.District.City.Id);
            ViewBag.DistrictList = new SelectList(await district.getDistrictsAsync(), "Id", "Name", emp.DistrictId);



            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeDTO employeevm)
        {


            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name", employeevm.DepartmentId);
            ViewBag.CountryList = new SelectList(await country.getCountriesAsync(), "Id", "Name",employeevm.CountryName);

            try
            {
                if (ModelState.IsValid)
                {

                    await employee.CreateOrUpdateEmployeeAsync(employeevm);



                    return RedirectToAction("Index");
                }

            }
           catch(Exception ex)
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




    }



    }

