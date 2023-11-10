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
        private Contexto context;

        public UsuarioBD(Contexto contexto)
        {
            context = contexto;
        }

        public void CrearCuenta(String Nombre = "", String Clave = "")
        {
            //Se supone que el Id lo crear el entity framework
            if(Nombre == "" && Clave == "")
            {
                Nombre = AnsiConsole.Ask<string>("Ingresa tu nombre: ");
                Clave = AnsiConsole.Ask<string>("Ingresa una clave:");
            }


            // using var db  = new Contexto(); Conexión a la BD --> contexto

            

            var nuevoUsuario = new Usuario { nombre = Nombre, clave = Clave };
            context.Usuarios.Add(nuevoUsuario);
            context.SaveChanges();

            //int nuevoUsuarioId = nuevoUsuario.id;
            //CuentaDeAhorroBD.CrearCuentaDeAhorro(nuevoUsuarioId);

            //MenuManager.MainMenuManagement();
        }

        public List<Usuario> ObtenerUsuarios()
        {
          //  using var db = new Contexto();
            var usuarios = context.Usuarios.ToList();
            return usuarios;
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            //using var db = new Contexto();
            var usuario = context.Usuarios.SingleOrDefault(u => u.id == id);
            return usuario;
        }

        public void MostrarInformacionCuenta(Usuario usuario)
        {
            //using var db = new Contexto();
            var cuentaDeAhorro = context.CuentasDeAhorros.SingleOrDefault(x => x.id_propietario == usuario.id);

            //Console.Clear();
            Console.WriteLine($"El propietario de la cuenta es: {usuario.nombre}");
            Console.WriteLine($"El saldo de la cuenta es: {cuentaDeAhorro.saldo}");
            //Console.WriteLine();
            //MenuManager.GestionarMenuMiCuenta(usuario);
        }
    }
}
