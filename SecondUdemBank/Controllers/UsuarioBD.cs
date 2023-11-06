using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace SecondUdemBank
{
    public class UsuarioBD
    {
        public static void CrearCuenta()
        {
            //Se supone que el Id lo crear el entity framework
            var Nombre = AnsiConsole.Ask<string>("Ingresa tu nombre: ");
            var Clave = AnsiConsole.Ask<string>("Ingresa una clave:");

            using var db = new Contexto(); //Conexión a la BD --> contexto
            var nuevoUsuario = new Usuario { nombre = Nombre, clave = Clave };
            db.Usuarios.Add(nuevoUsuario);
            db.SaveChanges();

            int nuevoUsuarioId = nuevoUsuario.id;
            CuentaDeAhorroBD.CrearCuentaDeAhorro(nuevoUsuarioId);

            MenuManager.MainMenuManagement();
        }

        public static List<Usuario> ObtenerUsuarios()
        {
            using var db = new Contexto();
            var usuarios = db.Usuarios.ToList();
            return usuarios;
        }

        public static Usuario ObtenerUsuarioPorId(int id)
        {
            using var db = new Contexto();
            var usuario = db.Usuarios.SingleOrDefault(u => u.id == id);
            return usuario;
        }

        public static void MostrarInformacionCuenta(Usuario usuario)
        {
            using var db = new Contexto();
            var cuentaDeAhorro = db.CuentasDeAhorros.SingleOrDefault(x => x.id_propietario == usuario.id);

            Console.Clear();
            Console.WriteLine($"El propietario de la cuenta es: {usuario.nombre}");
            Console.WriteLine($"El saldo de la cuenta es: {cuentaDeAhorro.saldo}");
            Console.WriteLine();
            MenuManager.GestionarMenuMiCuenta(usuario);
        }
    }
}
