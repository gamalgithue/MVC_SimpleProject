using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.DAL.Entities
{
    [Table("Countries")]
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
