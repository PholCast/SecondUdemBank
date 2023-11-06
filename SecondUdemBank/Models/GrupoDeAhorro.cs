using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class GrupoDeAhorro
    {
        public int id { get; set; }
        public int id_CreadorGrupo { get; set; }
        public double SaldoGrupo { get; set; }
        public string NombreGrupo { get; set; }
    }
}
