﻿using FirstPro.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Modals
{
    public class DistrictDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
