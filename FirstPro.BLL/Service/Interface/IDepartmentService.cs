using FirstPro.BLL.Modals;
using FirstPro.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Interface
{
   public interface IDepartmentService
    {
        public Task<IEnumerable<DepartmentDTO>> getDepartmentsAsync();

        public Task<IEnumerable<DepartmentDTO>> getDepartmentsAsync(int page, int pagesize);

        public Task<DepartmentDTO> getDepartmentAsync(int id);
        public Task CreateOrUpdateDepartmentAsync(DepartmentDTO department);
        public Task DeleteDepartmentAsync(DepartmentDTO department);



    }
}
