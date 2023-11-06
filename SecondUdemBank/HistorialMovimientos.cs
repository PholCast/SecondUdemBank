using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class HistorialMovimientos
    {
        public static void historialMovimientos(Usuario usuario)
        {
            var transaccionesPersonales = CuentaDeAhorroBD.ObtenerHistorialCuentaDeAhorro(usuario.id);

            if (transaccionesPersonales.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Historial de transacciones personales:");
                foreach (var transaccion in transaccionesPersonales)
                {
                    Console.WriteLine($"Fecha: {transaccion.fecha}, Tipo: {transaccion.TipoTransaccion}, Cantidad: {transaccion.CantidadTransaccion}");
                }
            }

            // Obtener transacciones de grupos de ahorro
            var transaccionesGrupo = UsuarioXGrupoAhorroBD.ObtenerHistorialGrupoDeAhorro(usuario.id);

            if (transaccionesGrupo.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Historial de transacciones de grupos de ahorro:");
                foreach (var transaccion in transaccionesGrupo)
                {
                    Console.WriteLine($"Fecha: {transaccion.fecha}, Tipo: {transaccion.TipoTransaccion}, Cantidad: {transaccion.CantidadTransaccion}");
                }
            }

            // Obtener transacciones de los prestamos
            var prestamos = PrestamoBD.ObtenerHistorialPrestamo(usuario.id);

            if (prestamos.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Historial de Préstamos:");
                foreach (var prestamo in prestamos)
                {
                    Console.WriteLine($"Fecha del Préstamo: {prestamo.fechaPrestamo}, Cantidad Prestada: {prestamo.cantidadPrestamo}, Deuda Actual: {prestamo.deudaActual}");
                    Console.WriteLine();
                }
            }
            if (transaccionesPersonales.Count <= 0 && transaccionesGrupo.Count <= 0 && prestamos.Count <= 0)
            {
                Console.WriteLine("No tienes historial de movimientos");
                Console.WriteLine();
            }

            MenuManager.GestionarMenuUsuario(usuario);
        }
    }
}
