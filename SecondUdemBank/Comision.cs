using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class Comision
    {
        public static void ObtenerComisionDeGrupoDisuelto(GrupoDeAhorro grupoDeAhorro)
        {
            double comision = grupoDeAhorro.SaldoGrupo * 0.05;
            udemBankBD.IngresarComisiones(comision);
            Console.WriteLine($"El banco ha obtenido {comision} de comisión");
        }

        public static void ObtenerComisionDeTransaccion(double saldo)
        {
            double comision = saldo * 0.001;
            udemBankBD.IngresarComisiones(comision);
            Console.WriteLine($"El banco ha obtenido {comision} de comisión");
        }
    }
}