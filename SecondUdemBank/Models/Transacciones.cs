using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class Transacciones
    {
        [Key]
        public int id { get; set; }

        public int id_cuentaDeAhorro { get; set; }
        [ForeignKey(nameof(id_cuentaDeAhorro))]
        public CuentaDeAhorro CuentaDeAhorro { get; set; }

        [Required]
        public double CantidadTransaccion { get; set; }

        [Required]
        public DateOnly fecha { get; set; }

        [Required]
        public string TipoTransaccion { get; set; }
    }
}
