using FirstPro.BLL.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Interface
{
  public interface ICountryService
    {
        public Task<IEnumerable<CountryDTO>> getCountriesAsync();
    }
}
