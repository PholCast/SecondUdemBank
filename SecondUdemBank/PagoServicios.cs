using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondUdemBank
{
    public class PagoServicios
    {

        /*public static (double,int,DateOnly) RealizarPagoPrestamo(Usuario usuario)
        {
            //var grupos = ObtenerGruposPorPagar(usuario);

            if(grupos == null)
            {
                DateOnly date = default(DateOnly);
                return (0,0,date);
            }

            var idGrupo = SeleccionarGrupoPorPagar(grupos);

            var relacionUsuarioGrupo = UsuarioXGrupoAhorroBD.ObtenerUsuarioXGrupoId(usuario.id, idGrupo); //id del UsuarioXGrupo

            var prestamosGrupo = PrestamoBD.ObtenerPrestamosUsuarioxGrupo(relacionUsuarioGrupo);

            var prestamoSeleccionado = SeleccionarPrestamo(prestamosGrupo);

            Console.WriteLine($"Deuda actual del prestamo: {prestamoSeleccionado.deudaActual}");

            double cuota = PrestamoBD.ActualizarPago(prestamoSeleccionado.id);

            DateOnly fechaDeCuota = PrestamoBD.ObtenerFechaCuota(prestamoSeleccionado.id);

            Console.WriteLine($"El numero de cuota en el que va es: {fechaDeCuota}");



            return (cuota,relacionUsuarioGrupo,fechaDeCuota);

        
        }*/





        /*public static List<GrupoDeAhorro> ObtenerGruposPorPagar(Usuario usuario)
        {
            List<Prestamo> prestamos = PrestamoBD.ObtenerPrestamosVigentes(usuario.id);

            if(prestamos.Count == 0)
            {
                
                return null;
            }

            var usuarioXGrupos = prestamos.Select(x => x.id_usuarioXGrupoDeAhorro).ToList();

            foreach(int id in usuarioXGrupos)
            {
                Console.WriteLine("Estos son los id: "+id);
            }
            var idGrupos = UsuarioXGrupoAhorroBD.ObtenerIdGrupos(usuarioXGrupos);
            

            //var grupos = GrupoDeAhorroBD.ObtenerGruposAhorro(idGrupos);


            return grupos;
            



        }*/

        public static int SeleccionarGrupoPorPagar(List<GrupoDeAhorro> grupos)
        {
            var nombresGrupos = grupos.Select(x => x.NombreGrupo).ToList();


            var opcion = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Selecciona un grupo de ahorro: ")
                    .AddChoices(nombresGrupos));

            return grupos.Single(x => x.NombreGrupo == opcion).id;
        }




        public static Prestamo SeleccionarPrestamo(List<Prestamo> prestamos)
        {
            //Para mostrar las opciones de prestamos que tiene por pagar, guardando un i que hace referencia al indice  y en otro la informacion del prestamo

            var infoPrestamos = prestamos.Select((p,index) => new {i = index,  infoPrestamo = $"Prestamo de: {p.cantidadPrestamo} de cuota {p.cantidadCuota}"} ).ToList();


            var opcion = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Selecciona un Prestamo: ")
                    .AddChoices(infoPrestamos.Select(p => p.infoPrestamo).ToList() )); //Este select se usa para mostrar solo que prestamos son con su informacion

            var prestamoSeleccionado = infoPrestamos.First(p => p.infoPrestamo == opcion);

            int indexPrestamoSeleccionado = prestamoSeleccionado.i;

            return prestamos[indexPrestamoSeleccionado]; //Retornamos el prestamo seleccionado

        }
        }
    }

