using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class Login
    {
       /* public static Usuario ObtenerListaUsuarios(int IdUsuario = -1)
        {
            var usuarios = UsuarioBD.ObtenerUsuarios();

            if (IdUsuario == -1)
            {
                var ListaUsuarios = usuarios.Select(x => x.nombre).ToArray();
                var opcion = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Selecciona un usuario")
                    .AddChoices(ListaUsuarios));

                var id = usuarios.Single(x => x.nombre == opcion).id;
                //var usuario = UsuarioBD.ObtenerUsuarioPorId(id);
                return usuario;
            }
            else
            {
                var ListaUsuarios = usuarios.Where(u => u.id != IdUsuario).Select(x => x.nombre).ToArray();

                if (ListaUsuarios.Length == 0)
                {
                    Console.WriteLine("No hay otros usuarios disponibles.");
                    return null;
                }

                var opcion = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Selecciona un usuario")
                    .AddChoices(ListaUsuarios));

                var id = usuarios.Single(x => x.nombre == opcion).id;
                //var usuario = UsuarioBD.ObtenerUsuarioPorId(id);
                //return usuario;
            }
        }*/

       /* public static Usuario Acceder()
        {
            String claveIngresada;
            Usuario usuario;
            do
            {
                usuario = ObtenerListaUsuarios();
                claveIngresada = AnsiConsole.Ask<string>("Ingresa tu clave: ");

                if (claveIngresada == usuario.clave)
                {
                    Console.WriteLine("Accediendo al sistema...");
                }
                else
                {
                    Console.WriteLine("Grave... Clave incorrecta.");
                }
            } while (claveIngresada != usuario.clave);

            return usuario;  
        }*/

        public static GrupoDeAhorro SeleccionarMiGrupoAhorro(int idUsuario)
        {
            var misUsuarioXGrupoAhorros = UsuarioXGrupoAhorroBD.ObtenerListaMisGrupos(idUsuario);
            if (misUsuarioXGrupoAhorros.Count != 0)
            {
                var listaMisUsuarioXGrupoAhorros = misUsuarioXGrupoAhorros.Select(x => x.id_GrupoAhorro).ToList(); //Lista con los id de los grupos

                var gruposAhorro = GrupoDeAhorroBD.ObtenerGruposAhorro(listaMisUsuarioXGrupoAhorros); 

                var nombresGrupoAhorro = gruposAhorro.Select(x => x.NombreGrupo).ToList();

                var opcion = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Selecciona un grupo de ahorro: ")
                    .AddChoices(nombresGrupoAhorro));

                var idGrupo = gruposAhorro.Single(x => x.NombreGrupo == opcion).id;
                return GrupoDeAhorroBD.ObtenerGrupoAhorroId(idGrupo);
            }
            else
            {
                return null;
            }
        }

        public static GrupoDeAhorro BuscarOtrosGrupos(int usuarioId)
        {
            var misUsuarioXGrupoAhorros = UsuarioXGrupoAhorroBD.ObtenerListaMisGrupos(usuarioId);
            if (misUsuarioXGrupoAhorros.Count != 0)
            {
                var listaMisUsuarioXGrupoAhorros = misUsuarioXGrupoAhorros.Select(x => x.id_GrupoAhorro).ToList(); //Lista con los id de los grupos
                List<int> idUsuarios = new List<int>();

                //Dos ciclos. El grande obtiene los id de los participantes de un grupo. Y el de adentro mete cada id
                //y verifica que no este antes de hacerlo
                foreach(int idGrupoAhorro in listaMisUsuarioXGrupoAhorros)
                {
                    List<int> auxiliarIdUsuarios = UsuarioXGrupoAhorroBD.ObtenerUsuariosGrupo(idGrupoAhorro,usuarioId);
                    foreach(int idUsuarioParticipante in auxiliarIdUsuarios)
                    {
                        if (!idUsuarios.Contains(idUsuarioParticipante))
                        {
                            idUsuarios.Add(idUsuarioParticipante);
                        }
                    }
                }
                
                if (idUsuarios.Count==0)
                {
                    Console.WriteLine("No hay usuarios en tus grupos de ahorro. Por lo tanto no puedes prestar de otros grupos");
                    return null;
                }
                //Ahora teniendo los usuarios, necesito obtener todos los grupos en los que estan esos usuarios. con el metodo mis grupos

                List<int> idGrupos = new List<int>();
                foreach(int idDeUsuario in idUsuarios)
                {
                    List<int> auxiliarIdGrupos = UsuarioXGrupoAhorroBD.ObtenerListaMisGrupos(idDeUsuario).Select(x => x.id_GrupoAhorro).ToList();
                    foreach(int idDeGrupo in auxiliarIdGrupos)
                    {
                        if (!idGrupos.Contains(idDeGrupo))
                        {
                            idGrupos.Add(idDeGrupo);
                        }   
                    }
                }

                //Ahora de esa lista hay que eliminar en los que ya esta el usuario
                /*foreach(int idGrupo in idGrupos)
                {
                    if (listaMisUsuarioXGrupoAhorros.Contains(idGrupo))
                    {
                        idGrupos.Remove(idGrupo);
                    }
                }
                //Aqui ya acabo y tiene los id de los otros grupos en los que puede prestar*/
                
                var idGruposFiltrados = idGrupos.Except(listaMisUsuarioXGrupoAhorros).ToList(); //Quita los elementos que ya estan en la otra lista
               
                // ya que con un ciclo da error porque va iterando sobre ella
                if(idGruposFiltrados.Count==0)
                {
                    Console.WriteLine("No puedes prestar de otros grupos porque tus amigos no estan en grupos diferentes a los tuyos");
                    return null;
                }

                return SeleccionarOtrosGrupos(idGruposFiltrados);
            }
            else
            {
                Console.WriteLine("No puede solicitar prestamos porque no estas en ningun grupo");
                return null;
            }
        }

        public static GrupoDeAhorro SeleccionarOtrosGrupos(List<int> idDeGrupos)
        {
            //Esto esta repetido, luego hay que quitarlo
            var gruposAhorro = GrupoDeAhorroBD.ObtenerGruposAhorro(idDeGrupos);
            var nombresGrupoAhorro = gruposAhorro.Select(x => x.NombreGrupo).ToList();

            var opcion = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Selecciona un grupo de ahorro: ")
                .AddChoices(nombresGrupoAhorro));

            var idGrupo = gruposAhorro.Single(x => x.NombreGrupo == opcion).id;
            return GrupoDeAhorroBD.ObtenerGrupoAhorroId(idGrupo);
        }
    }
}
