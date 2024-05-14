using FirstPro.BLL.Modals;
using FirstPro.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Interface
{
  public interface IEmployeeService
    {

        public Task<IEnumerable<EmployeeDTO>> getEmployeesAsync(int page, int pagesize);

        public Task<EmployeeDTO> getEmployeeAsync(int id);
        public Task CreateOrUpdateEmployeeAsync(EmployeeDTO employee);
        public Task DeleteEmployeeAsync(EmployeeDTO employee);



    }
}
