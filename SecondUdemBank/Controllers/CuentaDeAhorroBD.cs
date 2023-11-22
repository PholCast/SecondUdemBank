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
        private Contexto context;

        public CuentaDeAhorroBD(Contexto contexto)
        {
            context = contexto;
        }

        public void CrearCuentaDeAhorro(int id, double Saldo = -1)
        {
            if (Saldo == -1) { 
             Saldo = AnsiConsole.Ask<double>("Ingresa tu saldo inicial: "); }

            context.CuentasDeAhorros.Add(new CuentaDeAhorro { id_propietario = id, saldo = Saldo });
            context.SaveChanges();
        }

        public void IngresarCapital(CuentaDeAhorro cuentaDeAhorro, double saldoIngresado = -1,bool prestamo =false)
        {

            if (saldoIngresado ==-1)
            {
                saldoIngresado = AnsiConsole.Ask<double>("Ingresa la cantidad de saldo: "); 
            }
            if (saldoIngresado <= 0)
            {
                Console.WriteLine("Saldo invalido");
                return;
            }
            else
            {
                double saldoSinComision = saldoIngresado * 0.999;
                cuentaDeAhorro.saldo += saldoSinComision;
                //Comision.ObtenerComisionDeTransaccion(saldoIngresado);
                //TransaccionesBD.RegistrarTransaccionCuenta(cuentaDeAhorro.id, saldoSinComision, "Transación cuenta de ahorro");
                context.SaveChanges();

                Console.WriteLine("El saldo se ha actualizado correctamente");

                /*if (!prestamo)
                {
                    MenuManager.GestionarMenuUsuario(usuario);
                }*/
            }
        }

        public List<Transacciones> ObtenerHistorialCuentaDeAhorro(int idUsuario)
        {

            // Obtener transacciones personales
            var transaccionesPersonales = context.TransaccionesCuentaAhorros
                .Where(t => t.CuentaDeAhorro.id_propietario == idUsuario)
                .ToList();

            return transaccionesPersonales;
        }
    }
}
