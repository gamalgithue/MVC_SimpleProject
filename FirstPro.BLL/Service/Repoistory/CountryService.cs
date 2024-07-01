using AutoMapper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using FirstPro.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Repoistory
{
  public class CountryService:ICountryService
    {
        #region prop
        private readonly IGenericRepository<Country> _countryservice;
        private readonly IMapper mapper;
        #endregion
        #region ctor
        public CountryService(IGenericRepository<Country> _countryservice, IMapper _mapper)
        {
            this._countryservice = _countryservice;
            this.mapper = _mapper;
        }
        #endregion
        public async Task<IEnumerable<CountryDTO>> getCountriesAsync()
        {
            var result = await _countryservice.GetAsync(null,null,10,false);
            return mapper.Map<IEnumerable<CountryDTO>>(result);
        }
    }
}
