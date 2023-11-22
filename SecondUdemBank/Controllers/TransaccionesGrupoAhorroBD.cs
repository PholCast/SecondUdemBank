using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Spectre.Console;

namespace SecondUdemBank
{
    public class TransaccionesGrupoAhorroBD
    {
        public static double ObtenerAporteUsuario(int idUsuarioGrupo)
        {
            using var db = new Contexto();
            var transacciones = db.TransaccionesGruposAhorros
                                       .Where(t => t.idUsuarioXGrupo == idUsuarioGrupo && t.TipoTransaccion == "Transaccion Grupo de Ahorro").ToList();
            double sumaTransacciones = transacciones.Sum(t => t.CantidadTransaccion);
            Console.WriteLine($"Ha aportado: {sumaTransacciones}");
            return sumaTransacciones;
        }

        public static void RegistrarTransaccionGrupo(int idUsuarioGrupo, double cantidadTransaccion, string Tipo,DateOnly fechaActual = default)
        {
            using var db = new Contexto();
            

            //fechaActual se evalua esta condicion para ver si avanzan los meses del prestamo o si toma la actual
            if (fechaActual == default)
            {
                fechaActual = DateOnly.FromDateTime(DateTime.Now); // Si no se proporciona un valor, se usa la fecha actual
            }
            db.TransaccionesGruposAhorros.Add(new TransaccionesGrupoAhorro { idUsuarioXGrupo = idUsuarioGrupo, CantidadTransaccion = cantidadTransaccion, fecha =fechaActual, TipoTransaccion = Tipo});
            db.SaveChanges();
        }


        /*public static void RegistrarPagoPrestamo(Usuario usuario)
        {
            var infoTransaccion = PagoServicios.RealizarPagoPrestamo(usuario);

            if(infoTransaccion.Item1 == 0 && infoTransaccion.Item2 == 0)
            {
                Console.WriteLine("No tienes prestamos vigentes");
            }
            else
            {
                RegistrarTransaccionGrupo(infoTransaccion.Item2, infoTransaccion.Item1, "Pago Prestamo",infoTransaccion.Item3);

            }

            MenuManager.GestionarMenuUsuario(usuario);
        }*/
    }
}
