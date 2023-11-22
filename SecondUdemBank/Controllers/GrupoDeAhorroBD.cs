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
        private Contexto context;

        public GrupoDeAhorroBD(Contexto contexto)
        {
            context = contexto;
        }
        public void CrearGrupoDeAhorro(Usuario usuario, string nombreGrupo)
        {
            // using var db = new Contexto(); // Ya no es necesario
            var grupoAhorro = new GrupoDeAhorro { id_CreadorGrupo = usuario.id, SaldoGrupo = 0, NombreGrupo = nombreGrupo };

            context.GruposDeAhorros.Add(grupoAhorro);
            context.SaveChanges();

            // UsuarioXGrupoAhorroBD.UnirseAGrupoDeAhorro(usuario.id, grupoAhorro.id);

            // MenuManager.GestionarMenuMisGruposDeAhorro(usuario);
        }

        public List<GrupoDeAhorro> ObtenerGruposAhorro(List<int> listId)
        {
            List<GrupoDeAhorro> Grupos = new List<GrupoDeAhorro>();
            foreach (int id in listId)
            {
                GrupoDeAhorro grupo = context.GruposDeAhorros.FirstOrDefault(g => g.id == id);
                Grupos.Add(grupo);
            }
            return Grupos;
        }

        public GrupoDeAhorro ObtenerGrupoAhorroPorId(int id)
        {
            // using var db = new Contexto(); // Ya no es necesario
            var grupo = context.GruposDeAhorros.SingleOrDefault(u => u.id == id);
            return grupo;
        }

        public  void IncrementarSaldo(int id,double saldo)
        {
            var grupo = context.GruposDeAhorros.SingleOrDefault(u => u.id == id);

            if (grupo != null)
            {
                grupo.SaldoGrupo += saldo;
                context.SaveChanges();
            }    
        }
        public void QuitarSaldo(int id, double saldo)
        {
            var grupo = context.GruposDeAhorros.SingleOrDefault(u => u.id == id);

            if (grupo != null)
            {
                grupo.SaldoGrupo -= saldo;
                context.SaveChanges();
            }
        }

        public bool VerificarPropietario(Usuario usuario, GrupoDeAhorro grupoDeAhorro)
        {
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

        public void IngresarUsuarioAGrupoDeAhorro(Usuario usuario, Usuario usuarioInvitado, GrupoDeAhorro grupoDeAhorro)
        {

            // Verifica si el usuario ya pertenece al grupo de ahorro
            var Pertenece = context.UsuariosXGruposAhorros.SingleOrDefault(ug => ug.id_ParticipanteGrupo == usuarioInvitado.id && ug.id_GrupoAhorro == grupoDeAhorro.id);

            if (Pertenece != null)
            {
                Console.WriteLine($"{usuarioInvitado.nombre} ya es miembro del grupo de ahorro {grupoDeAhorro.NombreGrupo}.");
            }
            else
            {
                UsuarioXGrupoAhorroBD.UnirseAGrupoDeAhorro(usuarioInvitado.id, grupoDeAhorro.id);
                Console.WriteLine($"{usuarioInvitado.nombre} ha sido agregado al grupo de ahorro {grupoDeAhorro.NombreGrupo}");
            }
            //MenuManager.GestionarMenuGrupoDeAhorro(usuario, grupoDeAhorro);
        }

        public double ObtenerSaldoGrupo(int id)
        {
            var grupo = context.GruposDeAhorros.SingleOrDefault(u => u.id == id);
            return grupo.SaldoGrupo;
        }

        public int ObtenerCantidadGruposDeAhorro(int idUsuario)
        {

            int cantidadGruposAhorro = context.UsuariosXGruposAhorros
                .Where(x => x.id_ParticipanteGrupo == idUsuario && x.PerteneceAlGrupo)
                .Count();

            return cantidadGruposAhorro;
        }

        public bool ObtenerCantidadUsuarios(GrupoDeAhorro grupoDeAhorro)
        {

            int cantidadUsuarios = context.UsuariosXGruposAhorros
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

        public GrupoDeAhorro IngresarFidelizacion()
        {

            var grupos = context.GruposDeAhorros.ToList();

            if (grupos.Count == 0)
            {
                Console.WriteLine("No hay grupos de ahorro");
                //MenuManager.GestionarMenuFidelizacion();
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
            context.SaveChanges();

            return grupoConMayorSaldo;
        }
    }
}
