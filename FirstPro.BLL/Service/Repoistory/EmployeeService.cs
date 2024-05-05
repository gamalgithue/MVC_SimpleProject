using FirstPro.BLL.Service.Interface;
using FirstPro.DAL.Database;
using FirstPro.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Repoistory
{
    public class EmployeeService:GenericRepository<Employee>,IEmployeeService
    {

        public EmployeeService(ApplicationDbContext db):base(db)
        {
        }
    }
}
