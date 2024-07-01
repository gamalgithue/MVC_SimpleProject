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
   public class CityService:ICityService
    {
        #region prop
        private readonly IGenericRepository<City> _cityservice;
        private readonly IMapper mapper;
        #endregion
        #region ctor
        public CityService(IGenericRepository<City> _cityservice, IMapper _mapper)
        {
            this._cityservice = _cityservice;
            this.mapper = _mapper;
        }

        
        #endregion
        public async Task<IEnumerable<CityDTO>> getCitiesAsync(int id)
        {
            var result = await _cityservice.GetAsync(x=>x.CountryId==id);
            return mapper.Map<IEnumerable<CityDTO>>(result);
        }
    }
}
