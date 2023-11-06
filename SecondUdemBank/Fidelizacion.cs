using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class Fidelizacion
    {
        public static void FidelizacionGrupoDeAhorro()
        {
            GrupoDeAhorro grupoDeAhorro = GrupoDeAhorroBD.IngresarFidelizacion();

            Console.WriteLine($"El grupo con el saldo más alto es: {grupoDeAhorro.NombreGrupo} ha recibido un incremento del 10%.");
            MenuManager.GestionarMenuFidelizacion();
        }

    }
}
