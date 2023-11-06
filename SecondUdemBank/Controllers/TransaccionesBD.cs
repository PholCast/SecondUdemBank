using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class TransaccionesBD
    {
        public static void RegistrarTransaccionCuenta(int idCuentaDeAhorr, double cantidadTransaccion, string Tipo)
        {
            using var db = new Contexto();

            DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);
            db.TransaccionesCuentaAhorros.Add(new Transacciones { id_cuentaDeAhorro = idCuentaDeAhorr, CantidadTransaccion = cantidadTransaccion, fecha = fechaActual, TipoTransaccion = Tipo });
            db.SaveChanges();
        }
    }
}
