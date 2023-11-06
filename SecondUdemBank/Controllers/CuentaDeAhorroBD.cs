using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;


namespace SecondUdemBank
{
    public class CuentaDeAhorroBD
    {
        public static void CrearCuentaDeAhorro(int id)
        {
            var Saldo = AnsiConsole.Ask<double>("Ingresa tu saldo inicial: ");

            using var db = new Contexto(); //Conexión a la BD --> contexto
            db.CuentasDeAhorros.Add(new CuentaDeAhorro { id_propietario = id, saldo  = Saldo });
            db.SaveChanges();
        }

        public static void IngresarCapital(Usuario usuario, double saldoIngresado = -1,bool prestamo =false)
        {
            using var db = new Contexto();

            var cuentaDeAhorro = db.CuentasDeAhorros.SingleOrDefault(x => x.id_propietario == usuario.id);
            
            if(saldoIngresado ==-1)
            {
                saldoIngresado = AnsiConsole.Ask<double>("Ingresa la cantidad de saldo: "); 
            }
            if (saldoIngresado <= 0)
            {
                Console.WriteLine("Saldo invalido");
                MenuManager.GestionarMenuMiCuenta(usuario);
            }
            else
            {
                double saldoSinComision = saldoIngresado * 0.999;
                cuentaDeAhorro.saldo += saldoSinComision;
                Comision.ObtenerComisionDeTransaccion(saldoIngresado);
                TransaccionesBD.RegistrarTransaccionCuenta(cuentaDeAhorro.id, saldoSinComision, "Transación cuenta de ahorro");
                db.SaveChanges();
                Console.WriteLine("El saldo se ha actualizado correctamente");
                if (prestamo == true)
                {
                    return;
                }
                MenuManager.GestionarMenuUsuario(usuario);
            }
        }

        public static List<Transacciones> ObtenerHistorialCuentaDeAhorro(int idUsuario)
        {
            using var db = new Contexto();

            // Obtener transacciones personales
            var transaccionesPersonales = db.TransaccionesCuentaAhorros
                .Where(t => t.CuentaDeAhorro.id_propietario == idUsuario)
                .ToList();

            return transaccionesPersonales;
        }
    }
}
