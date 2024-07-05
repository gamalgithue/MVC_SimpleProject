using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.DAL.Entities
{
    [Table("Districts")]
   public class District
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
