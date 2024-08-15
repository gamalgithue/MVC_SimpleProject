using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Helper
{
    public class ApiResponse<T>
    {

        public int Code { get; set; }
        public string Status { get; set; }
        public T Result { get; set; }
    }
}
