using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondUdemBank
{
    public class PrestamoServicios
    {
        public static (double SaldoPrestamo, int idUxG, int CantidadMeses)? SolicitarCantidad(Usuario usuario, GrupoDeAhorro grupo)
        {
            var saldoPrestar = SolicitarSaldo();

            int idUsuarioxGrupo = UsuarioXGrupoAhorroBD.ObtenerUsuarioXGrupoId(usuario.id, grupo.id);

            bool verificarCantidad = VerificarCantidadPrestamo(saldoPrestar, idUsuarioxGrupo, grupo);

            if (verificarCantidad)
            {
                Comision.ObtenerComisionDeTransaccion(saldoPrestar);
                var cantidadMeses = SolicitarCantidadMesesPrestamo();

                var tupla = (SaldoPrestamo: saldoPrestar, idUxG: idUsuarioxGrupo, CantidadMeses: cantidadMeses);

                MostrarDatosPrestamo(saldoPrestar,idUsuarioxGrupo,cantidadMeses);
                return tupla;
            }
            else { return null; }
        }

        public static double SolicitarSaldo()
        {
            var saldoPrestar = -1.0;
            do
            {
                saldoPrestar = AnsiConsole.Ask<double>("Ingresa la cantidad de saldo a Prestar: ");

                if (saldoPrestar <= 0)
                {
                    Console.WriteLine("Error, ingresa una cantidad mayor a cero");
                }
            } while (saldoPrestar <= 0);

            double saldoaPrestarSinComision = saldoPrestar * 0.999;
            
            return saldoaPrestarSinComision;
        }

        public static void MostrarDatosPrestamo(double saldo, int id, int meses)
        {
            DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);
            DateOnly fechaDePago = fechaActual.AddMonths(meses);
            Console.WriteLine($"Datos del prestamo:\nFecha actual: {fechaActual}\nSaldo a Prestar: {saldo}\nFecha de pago{fechaDePago}\nPresiona Enter para continuar ");
            Console.ReadKey();
        }

        public static int SolicitarCantidadMesesPrestamo()
        {
            var cantidadMeses = 0;
            do
            {
                cantidadMeses = AnsiConsole.Ask<int>("Ingresa la cantidad de meses a pagar: ");

                if (cantidadMeses <= 2)
                {
                    Console.WriteLine("Error, el plazo de pago debe ser mayor a 2 meses ");
                }
            } while (cantidadMeses <= 2);

            return cantidadMeses;
        }

        public static bool VerificarCantidadPrestamo(double saldoAPrestar, int idUsuarioGrupo, GrupoDeAhorro grupo)
        {
            var saldoGrupo = GrupoDeAhorroBD.ObtenerSaldoGrupo(grupo.id);
            if(saldoAPrestar  > saldoGrupo) {
                Console.WriteLine("ERROR: La cantidad a prestar es mayor al saldo del grupo");
                return false;
            }

            var saldoAportado = TransaccionesGrupoAhorroBD.ObtenerAporteUsuario(idUsuarioGrupo);

            if(saldoAPrestar > saldoAportado)
            {
                Console.WriteLine("ERROR: La cantidad a prestar es mayor a la que has aportado");
                return false;
            }

            return true;
        }

        //A partir de aqui los de otros grupos
        public static (double SaldoPrestamo, int idUxG, int CantidadMeses)? VerificarPrestamoOtrosGrupos(Usuario usuario, GrupoDeAhorro grupo)
        {
            double cantidadAPrestar = SolicitarSaldo();
            var saldoGrupo = GrupoDeAhorroBD.ObtenerSaldoGrupo(grupo.id);

            if (cantidadAPrestar > saldoGrupo)
            {
                Console.WriteLine("ERROR: La cantidad a prestar es mayor al saldo del grupo");
                return null;
            }
            else
            {
                int numeroMeses = SolicitarCantidadMesesPrestamo();

                int idUsuarioxGrupo = UsuarioXGrupoAhorroBD.CrearRelacionPrestamo(usuario.id, grupo.id);
                
                var tupla = (SaldoPrestamo: cantidadAPrestar, idUxG: idUsuarioxGrupo, CantidadMeses: numeroMeses);

                MostrarDatosPrestamo(cantidadAPrestar, idUsuarioxGrupo, numeroMeses);
                return tupla;
            }
        }
    }
}
