using AutoMapper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using FirstPro.DAL.Database;
using FirstPro.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Repoistory
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IGenericRepository<Employee> _employeeservice;
        private readonly IMapper _mapper;

        public EmployeeService(IGenericRepository<Employee> _employee,IMapper _mapper)
        {
            this._employeeservice = _employee;
            this._mapper = _mapper;
        }


        public async Task<IEnumerable<EmployeeDTO>> getEmployeesAsync(int page, int pagesize)
        {
            var result = await  _employeeservice.GetAsync(null, page, pagesize, false, x => x.Department);
            return _mapper.Map<IEnumerable<EmployeeDTO>>(result);
        }

        public async Task<EmployeeDTO> getEmployeeAsync(int id)
        {
            var result = await _employeeservice.GetFirstOrDefaultAsync(x => x.Id == id, false, x => x.Department);
            return _mapper.Map<EmployeeDTO>(result);
        }
        public async Task CreateOrUpdateEmployeeAsync(EmployeeDTO employee)
        {
            var obj = _mapper.Map<Employee>(employee);
            await _employeeservice.CreateOrUpdateAsync(obj);

        }



        public async Task DeleteEmployeeAsync(EmployeeDTO employee)
        {
            var obj = _mapper.Map<Employee>(employee);
            await _employeeservice.DeleteAsync(obj);
        }

       

        
    }
}
