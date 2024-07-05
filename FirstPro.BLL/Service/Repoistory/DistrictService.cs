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
  public class DistrictService:IDistrictService
    {
        #region prop
        private readonly IGenericRepository<District> _districtservice;
        private readonly IMapper mapper;
        #endregion
        #region ctor
        public DistrictService(IGenericRepository<District> _districtservice, IMapper _mapper)
        {
            this._districtservice = _districtservice;
            this.mapper = _mapper;
        }


        #endregion
        public async Task<IEnumerable<DistrictDTO>> getDistrictsAsync()
        {
            var result = await _districtservice.GetAsync();
            return mapper.Map<IEnumerable<DistrictDTO>>(result);
        }
        public async Task<IEnumerable<DistrictDTO>> getDistrictsAsync(int id)
        {
            var result = await _districtservice.GetAsync(x => x.CityId == id);
            return mapper.Map<IEnumerable<DistrictDTO>>(result);
        }
    }
}
