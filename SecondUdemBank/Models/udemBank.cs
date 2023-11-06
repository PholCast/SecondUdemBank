using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondUdemBank
{
    public class udemBank
    {
        [Key]
        public int id { get; set; }

        [Required]
        public double comision { get; set; }
    }
}
