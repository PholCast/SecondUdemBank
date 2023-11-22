using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class PrestamoBD
    {
        /*public static void PrestamoGrupoParticipante(Usuario usuario, GrupoDeAhorro grupo)
        {
            //Conexión a la BD --> contexto
            (double SaldoPrestamo, int idUxG, int cantidadMeses)? tuplaDatos = PrestamoServicios.SolicitarCantidad(usuario, grupo); //Una tupla que me retorne el valor y el id usuarioxgrupo, cantidadMeses

            if (tuplaDatos != null)
            {
                //Todos estos calculos hay que meterlos en otra funcion 
                Console.WriteLine("Tupla datos no fue null");
                double saldoPrestar = tuplaDatos.Value.SaldoPrestamo;
                int idUsuarioGrupo = tuplaDatos.Value.idUxG;
                int meses = tuplaDatos.Value.cantidadMeses;
                Console.WriteLine($"Datos retornados por tupla:saldoprestar:{saldoPrestar}\nidUxG:{idUsuarioGrupo}\nmeses:{meses}");

                DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);
                DateOnly fechaPago = DateOnly.FromDateTime(DateTime.Now).AddMonths(meses);

                double cantidadPagar = saldoPrestar + (saldoPrestar * 0.03);
                double cuota = cantidadPagar / meses;

                using var db = new Contexto();

                db.Prestamos.Add(new Prestamo
                {
                    id_usuarioXGrupoDeAhorro = idUsuarioGrupo,
                    cantidadPrestamo = saldoPrestar,
                    deudaActual = cantidadPagar,
                    cantidadCuota = cuota,
                    cantidadAPagar = cantidadPagar,
                    fechaPrestamo = fechaActual,
                    fechaPlazo = fechaPago,
                    interes = 0.03
                });
                db.SaveChanges();
                //CuentaDeAhorroBD.IngresarCapital(usuario, saldoPrestar, true);
                //GrupoDeAhorroBD.QuitarSaldo(grupo.id, saldoPrestar);

                Console.WriteLine("Prestamo Agregado");
                MenuManager.GestionarMenuUsuario(usuario);
            }
            else
            {
                MenuManager.GestionarMenuUsuario(usuario);
                return;
            }
        }*/

        /*public static void PrestamoOtrosGrupos(Usuario usuario, GrupoDeAhorro grupo)
        {
            (double SaldoPrestamo, int idUxG, int cantidadMeses)? tuplaDatos = PrestamoServicios.VerificarPrestamoOtrosGrupos(usuario, grupo); //Una tupla que me retorne el valor y el id usuarioxgrupo, cantidadMeses

            if (tuplaDatos != null)
            {
               
                double saldoPrestar = tuplaDatos.Value.SaldoPrestamo;
                int idUsuarioGrupo = tuplaDatos.Value.idUxG;
                int meses = tuplaDatos.Value.cantidadMeses;
                //Console.WriteLine($"Datos retornados por tupla:saldoprestar:{saldoPrestar}\nidUxG:{idUsuarioGrupo}\nmeses:{meses}");

                DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);
                DateOnly fechaPago = DateOnly.FromDateTime(DateTime.Now).AddMonths(meses);

                double cantidadPagar = saldoPrestar + (saldoPrestar * 0.05);
                double cuota = cantidadPagar / meses;

                using var db = new Contexto();

                db.Prestamos.Add(new Prestamo
                {
                    id_usuarioXGrupoDeAhorro = idUsuarioGrupo,
                    cantidadPrestamo = saldoPrestar,
                    deudaActual = cantidadPagar,
                    cantidadCuota = cuota,
                    cantidadAPagar = cantidadPagar,
                    fechaPrestamo = fechaActual,
                    fechaPlazo = fechaPago,
                    interes = 0.05
                });
                db.SaveChanges();
                //CuentaDeAhorroBD.IngresarCapital(usuario, saldoPrestar);
                //GrupoDeAhorroBD.QuitarSaldo(grupo.id, saldoPrestar);

                Console.WriteLine("Prestamo Agregado");
                MenuManager.GestionarMenuUsuario(usuario);
            }
            else
            {
                //Console.WriteLine("tupla datos para prestamos otrosgrupos fue null");
                MenuManager.GestionarMenuUsuario(usuario);
                return;
            }
        }*/

        public static List<Prestamo> ObtenerHistorialPrestamo(int idUsuario)
        {
            using var db = new Contexto();

            var prestamos = db.Prestamos
                .Where(p => p.usuarioXGrupoDeAhorro.Usuario.id == idUsuario)
                .ToList();

            return prestamos;
        }



        public static List<Prestamo> ObtenerPrestamosVigentes(int idUsuario)
        {
            using var db = new Contexto();

            var prestamos = db.Prestamos
                .Where(p => p.usuarioXGrupoDeAhorro.Usuario.id == idUsuario && p.deudaActual > 0)
                .ToList();

            return prestamos;
        }



        public static List<Prestamo> ObtenerPrestamosUsuarioxGrupo(int idUsuarioxGrupo)
        {
            using var db = new Contexto();

            var prestamos = db.Prestamos
                .Where(p => p.id_usuarioXGrupoDeAhorro == idUsuarioxGrupo && p.deudaActual > 0)
                .ToList();

            return prestamos;

        }

        public static double ActualizarPago(int idPrestamo)
        {


            using var db = new Contexto();

            var prestamo = db.Prestamos.SingleOrDefault(p => p.id == idPrestamo);

            MostrarInfoPrestamo(prestamo);

            prestamo.deudaActual -= prestamo.cantidadCuota;

            db.SaveChanges();

            Console.WriteLine($"Has pagado ${prestamo.cantidadCuota}");

            return prestamo.cantidadCuota;


        }


        public static void MostrarInfoPrestamo(Prestamo prestamo)
        {
            Console.WriteLine($"Cantidad del prestamo: {prestamo.cantidadPrestamo}\n" +
                $"Deuda Actual: {prestamo.deudaActual}\n" +
                $"Cantidad Cuota: {prestamo.cantidadCuota}\n+" +
                $"Fecha del prestamo {prestamo.fechaPrestamo}\n" +
                $"Meses a pagar: {prestamo.cantidadAPagar / prestamo.cantidadCuota}\n" +
                $"Cantidad de meses que has pagado: {(int)Math.Round((prestamo.cantidadAPagar - prestamo.deudaActual) / prestamo.cantidadCuota)}\n" +
                $"Interes del prestamo: {prestamo.interes}");
        }

        public static DateOnly  ObtenerFechaCuota(int idPrestamo)
        {
            using var db = new Contexto();

            var prestamo = db.Prestamos.SingleOrDefault(p => p.id == idPrestamo);

            int numeroCuota = (int)Math.Round((prestamo.cantidadAPagar - prestamo.deudaActual) / prestamo.cantidadCuota);

            DateOnly fechaCuota = prestamo.fechaPrestamo.AddMonths(numeroCuota);

            return fechaCuota;


        }
    }
}