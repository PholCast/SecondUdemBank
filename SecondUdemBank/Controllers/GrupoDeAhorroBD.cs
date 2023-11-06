using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class GrupoDeAhorroBD
    {
        public static void CrearGrupoDeAhorro(Usuario usuario)
        {
            bool MaximoGruposDeAhorro = Restricciones.TieneMaximoGruposAhorro(usuario.id);
            if (MaximoGruposDeAhorro == true)
            {
                Console.WriteLine("Ya no puedes crear más grupos de ahorro");
            }
            else
            {
                var nombreGrupo = AnsiConsole.Ask<string>("Ingresa un nombre para el grupo de ahorro: ");

                using var db = new Contexto(); //Conexión a la BD --> contexto
                var grupoAhorro = new GrupoDeAhorro { id_CreadorGrupo = usuario.id, SaldoGrupo = 0, NombreGrupo = nombreGrupo };

                db.GruposDeAhorros.Add(grupoAhorro);
                db.SaveChanges();

                UsuarioXGrupoAhorroBD.UnirseAGrupoDeAhorro(usuario.id, grupoAhorro.id);
            }

            MenuManager.GestionarMenuMisGruposDeAhorro(usuario);
        }

        public static List<GrupoDeAhorro> ObtenerGruposAhorro(List<int> listId)
        {
            using var db = new Contexto();
            List<GrupoDeAhorro> Grupos = new List<GrupoDeAhorro>();
            foreach (int id in listId)
            {
                GrupoDeAhorro grupo = db.GruposDeAhorros.Single(g => g.id == id);
                Grupos.Add(grupo);
            }
            return Grupos;
        }

        public static GrupoDeAhorro ObtenerGrupoAhorroId(int id)
        {
            using var db = new Contexto();
            var grupo = db.GruposDeAhorros.SingleOrDefault(u => u.id == id);
            return grupo;
        }

        public static void IncrementarSaldo(int id,double saldo)
        {
            Console.WriteLine("Entre a agregar saldo al grupo");
            using var db = new Contexto();
            var grupo = db.GruposDeAhorros.SingleOrDefault(u => u.id == id);
            //var grupo = ObtenerGrupoAhorroId(id);
            grupo.SaldoGrupo += saldo;
            db.SaveChanges();
        }
        public static void QuitarSaldo(int id, double saldo)
        {
            Console.WriteLine("Entre a quitar saldo del grupo");
            using var db = new Contexto();
            var grupo = db.GruposDeAhorros.SingleOrDefault(u => u.id == id);
            //var grupo = ObtenerGrupoAhorroId(id);
            grupo.SaldoGrupo -= saldo;
            
            db.SaveChanges();
        }

        public static bool VerificarPropietario(Usuario usuario, GrupoDeAhorro grupoDeAhorro)
        {
            using var db = new Contexto();

            if (grupoDeAhorro.id_CreadorGrupo == usuario.id)
            {
                return true;
            }
            else
            {
                Console.WriteLine("No puedes invitar porque no eres el propietario de este grupo");
                return false;
            }
        }

        public static void IngresarUsuarioAGrupoDeAhorro(Usuario usuario, Usuario usuarioInvitado, GrupoDeAhorro grupoDeAhorro)
        {
            using var db = new Contexto();

            // Verifica si el usuario ya pertenece al grupo de ahorro
            var Pertenece = db.UsuariosXGruposAhorros.SingleOrDefault(ug => ug.id_ParticipanteGrupo == usuarioInvitado.id && ug.id_GrupoAhorro == grupoDeAhorro.id);

            if (Pertenece != null)
            {
                Console.WriteLine($"{usuarioInvitado.nombre} ya es miembro del grupo de ahorro {grupoDeAhorro.NombreGrupo}.");
            }
            else
            {
                bool MaximoGruposDeAhorro = Restricciones.TieneMaximoGruposAhorro(usuarioInvitado.id);
                if (MaximoGruposDeAhorro == true)
                {
                    Console.WriteLine($"El usuario {usuarioInvitado.nombre} ya no puede estar en más grupos de ahorro");
                }
                else
                {
                    UsuarioXGrupoAhorroBD.UnirseAGrupoDeAhorro(usuarioInvitado.id, grupoDeAhorro.id);
                    Console.WriteLine($"{usuarioInvitado.nombre} ha sido agregado al grupo de ahorro {grupoDeAhorro.NombreGrupo}");
                }
            }
            MenuManager.GestionarMenuGrupoDeAhorro(usuario, grupoDeAhorro);
        }

        public static double ObtenerSaldoGrupo(int id)
        {
            using var db = new Contexto();
            var grupo = db.GruposDeAhorros.SingleOrDefault(u => u.id == id);
            return grupo.SaldoGrupo;
        }

        public static int ObtenerCantidadGruposDeAhorro(int idUsuario)
        {
            using var db = new Contexto();

            int cantidadGruposAhorro = db.UsuariosXGruposAhorros
                .Where(x => x.id_ParticipanteGrupo == idUsuario && x.PerteneceAlGrupo)
                .Count();

            return cantidadGruposAhorro;
        }

        public static bool ObtenerCantidadUsuarios(GrupoDeAhorro grupoDeAhorro)
        {
            using var db = new Contexto();

            int cantidadUsuarios = db.UsuariosXGruposAhorros
                .Count(ug => ug.id_GrupoAhorro == grupoDeAhorro.id && ug.PerteneceAlGrupo);

            if (cantidadUsuarios <= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static GrupoDeAhorro IngresarFidelizacion()
        {
            using var db = new Contexto();

            var grupos = db.GruposDeAhorros.ToList();

            if (grupos.Count == 0)
            {
                Console.WriteLine("No hay grupos de ahorro");
                MenuManager.GestionarMenuFidelizacion();
            }

            GrupoDeAhorro grupoConMayorSaldo = grupos[0];
            foreach (var grupo in grupos)
            {
                if (grupo.SaldoGrupo > grupoConMayorSaldo.SaldoGrupo)
                {
                    grupoConMayorSaldo = grupo;
                }
            }

            double incremento = grupoConMayorSaldo.SaldoGrupo * 0.10;
            grupoConMayorSaldo.SaldoGrupo += incremento;
            db.SaveChanges();

            return grupoConMayorSaldo;
        }
    }
}
