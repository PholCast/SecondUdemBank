using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Spectre.Console;

namespace SecondUdemBank
{
    public class MenuManager
    {
        enum MainMenuOptions
        {
            IniciarSesion,
            Registrarse,
            Fidelizacion,
            Salir
        }

        enum MenuRegistrarse
        {
            CrearCuenta,
            Salir
        }

        enum MenuFidelizacion
        {
            FidelizacionPorGrupoDeAhorro,
            Salir
        }

        enum MenuPagos
        {
            PagarMisPrestamos,
            Salir
        }

        enum MenuUsuario
        {
            MiCuenta,
            Pagar,
            HistorialMovimientos,
            Prestamos,
            GestionarMisGruposDeAhorro,
            SalirDeCuenta
        }

        enum MenuMiCuenta
        {
            IngresarSaldoACuentaDeAhorro,
            Salir
        }

        enum MenuGestionarGruposDeAhorro
        {
            CrearGrupoDeAhorro,
            SeleccionarUnGrupoDeAhorro,
            Salir
        }

        enum MenuGrupoDeAhorro
        {
            InvitarUsuarioAGrupoDeAhorro,
            DisolverGrupoDeAhorro,
            IngresarCapitalAGrupoDeAhorro,
            Salir
        }

        enum MenuPrestamos
        {
            MisGruposDeAhorro,
            OtrosGrupos,
            Salir
        }

        public static void MainMenuManagement()
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("Bienvenido a UdemBank \nQué quieres hacer?")
                .AddChoices(
                    MainMenuOptions.IniciarSesion,
                    MainMenuOptions.Registrarse,
                    MainMenuOptions.Fidelizacion,
                    MainMenuOptions.Salir
                    ));

            switch (option)
            {
                case MainMenuOptions.IniciarSesion:
                    Usuario usuario = Login.Acceder();
                    GestionarMenuUsuario(usuario);
                    break;
                case MainMenuOptions.Registrarse:
                    GestionarMenuRegistrarse();
                    break;
                case MainMenuOptions.Fidelizacion:
                    GestionarMenuFidelizacion();
                    break;
                case MainMenuOptions.Salir:
                    Console.WriteLine("¡Gracias por usar UdemBank!");
                    break;
            }
        }

        public static void GestionarMenuRegistrarse()
        {
            Console.ReadKey();
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuRegistrarse>()
            .Title("Que quieres hacer?: ")
            .AddChoices(
                MenuRegistrarse.CrearCuenta,
                MenuRegistrarse.Salir));

            switch (option)
            {
                case MenuRegistrarse.CrearCuenta:
                    UsuarioBD.CrearCuenta();
                    break;
                case MenuRegistrarse.Salir:
                    MainMenuManagement();
                    break;
            }
        }

        public static void GestionarMenuFidelizacion()
        {
            Console.ReadKey();
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuFidelizacion>()
            .Title("Que quieres hacer?: ")
            .AddChoices(
                MenuFidelizacion.FidelizacionPorGrupoDeAhorro,
                MenuFidelizacion.Salir));

            switch (option)
            {
                case MenuFidelizacion.FidelizacionPorGrupoDeAhorro:
                    Fidelizacion.FidelizacionGrupoDeAhorro();
                    break;
                case MenuFidelizacion.Salir:
                    MainMenuManagement();
                    break;
            }
        }

        public static void GestionarMenuUsuario(Usuario usuario)
        {
            Console.ReadKey();
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuUsuario>()
                .Title("Qué quieres hacer?")
                .AddChoices(
                    MenuUsuario.MiCuenta,
                    MenuUsuario.Pagar,
                    MenuUsuario.HistorialMovimientos,
                    MenuUsuario.Prestamos,
                    MenuUsuario.GestionarMisGruposDeAhorro,
                    MenuUsuario.SalirDeCuenta
                    ));

            switch (option)
            {
                case MenuUsuario.MiCuenta:
                    UsuarioBD.MostrarInformacionCuenta(usuario);
                    break;
                case MenuUsuario.Pagar:
                    GestionarMenuPagos(usuario);
                    break;
                case MenuUsuario.HistorialMovimientos:
                    HistorialMovimientos.historialMovimientos(usuario);
                    break;
                case MenuUsuario.Prestamos:
                    GestionarMenuPrestamos(usuario);
                    break;
                case MenuUsuario.GestionarMisGruposDeAhorro:
                    GestionarMenuMisGruposDeAhorro(usuario);
                    break;
                case MenuUsuario.SalirDeCuenta:
                    MainMenuManagement();
                    break;
            }
        }

        public static void GestionarMenuMiCuenta(Usuario usuario)
        {
            Console.ReadKey();
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuMiCuenta>()
                .Title("Qué quieres hacer?")
                .AddChoices(
                    MenuMiCuenta.IngresarSaldoACuentaDeAhorro,
                    MenuMiCuenta.Salir
                    ));

            switch (option)
            {
                case MenuMiCuenta.IngresarSaldoACuentaDeAhorro:
                    CuentaDeAhorroBD.IngresarCapital(usuario);
                    break;
                case MenuMiCuenta.Salir:
                    GestionarMenuUsuario(usuario);
                    break;
            }
        }

        public static void GestionarMenuMisGruposDeAhorro(Usuario usuario)
        {
            Console.ReadKey();
            Console.Clear();
            var option = AnsiConsole.Prompt(
              new SelectionPrompt<MenuGestionarGruposDeAhorro>()
              .Title("Qué quieres hacer?")
              .AddChoices(
                  MenuGestionarGruposDeAhorro.CrearGrupoDeAhorro,
                  MenuGestionarGruposDeAhorro.SeleccionarUnGrupoDeAhorro,
                  MenuGestionarGruposDeAhorro.Salir
                  ));

            switch (option)
            {
                case MenuGestionarGruposDeAhorro.CrearGrupoDeAhorro:
                    GrupoDeAhorroBD.CrearGrupoDeAhorro(usuario);
                    break;
                case MenuGestionarGruposDeAhorro.SeleccionarUnGrupoDeAhorro:
                    GrupoDeAhorro miGrupo = Login.SeleccionarMiGrupoAhorro(usuario.id);
                    if (miGrupo != null)
                    {
                        GestionarMenuGrupoDeAhorro(usuario, miGrupo);
                    }
                    else
                    {
                        Console.WriteLine($"{usuario.nombre} no tiene grupos de ahorro");
                        GestionarMenuMisGruposDeAhorro(usuario);
                    }
                    break;
                case MenuGestionarGruposDeAhorro.Salir:
                    GestionarMenuUsuario(usuario);
                    break;
            }
        }

        public static void GestionarMenuGrupoDeAhorro(Usuario usuario, GrupoDeAhorro grupo)
        {
            Console.ReadKey();
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MenuGrupoDeAhorro>()
                .Title("Qué quieres hacer?")
                .AddChoices(
                    MenuGrupoDeAhorro.InvitarUsuarioAGrupoDeAhorro,
                    MenuGrupoDeAhorro.DisolverGrupoDeAhorro,
                    MenuGrupoDeAhorro.IngresarCapitalAGrupoDeAhorro,
                    MenuGrupoDeAhorro.Salir
                    ));

            switch (option)
            {
                case MenuGrupoDeAhorro.InvitarUsuarioAGrupoDeAhorro:
                    bool verificarPropiedad = GrupoDeAhorroBD.VerificarPropietario(usuario, grupo);
                    if (verificarPropiedad)
                    {
                        bool VerificarInvitacion = GrupoDeAhorroBD.ObtenerCantidadUsuarios(grupo);
                        if (VerificarInvitacion)
                        {
                            Usuario usuarioInvitado = Login.ObtenerListaUsuarios(usuario.id);
                            GrupoDeAhorroBD.IngresarUsuarioAGrupoDeAhorro(usuario, usuarioInvitado, grupo);
                        }
                        else
                        {
                            Console.WriteLine("Ya no puedes invitar a más personas en este grupo de ahorro");
                            GestionarMenuGrupoDeAhorro(usuario, grupo);
                        }

                    }
                    else
                    {
                        GestionarMenuGrupoDeAhorro(usuario, grupo);
                    }
                    break;
                case MenuGrupoDeAhorro.DisolverGrupoDeAhorro:
                    UsuarioXGrupoAhorroBD.DisolverGrupoDeAhorro(usuario, grupo);
                    break;
                case MenuGrupoDeAhorro.IngresarCapitalAGrupoDeAhorro:
                    UsuarioXGrupoAhorroBD.IngresarCapitalAGrupoDeAhorro(usuario, grupo);
                    break;
                case MenuGrupoDeAhorro.Salir:
                    GestionarMenuMisGruposDeAhorro(usuario);
                    break;
            }
        }

        public static void GestionarMenuPrestamos(Usuario usuario)
        {
            Console.ReadKey();
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuPrestamos>()
            .Title("Te encuentras en el menu de Prestamos, ¿A que grupo deseas prestar? ")
            .AddChoices(
                MenuPrestamos.MisGruposDeAhorro,
                MenuPrestamos.OtrosGrupos,
                MenuPrestamos.Salir));

            switch (option)
            {
                case MenuPrestamos.MisGruposDeAhorro:

                    //Aqui quedó repetido, hay que organizarlo
                    GrupoDeAhorro miGrupo = Login.SeleccionarMiGrupoAhorro(usuario.id);

                    if (miGrupo != null)
                    {
                        PrestamoBD.PrestamoGrupoParticipante(usuario, miGrupo);
                    }
                    else
                    {
                        Console.WriteLine($"{usuario.nombre} no tiene grupos de ahorro");
                        GestionarMenuUsuario(usuario);
                    }
                    break;
                case MenuPrestamos.OtrosGrupos:
                    GrupoDeAhorro otroGrupo = Login.BuscarOtrosGrupos(usuario.id);
                    if (otroGrupo != null)
                    {
                        Console.WriteLine("Niceeee");
                        PrestamoBD.PrestamoOtrosGrupos(usuario, otroGrupo);
                    }
                    else
                    {
                        GestionarMenuUsuario(usuario);
                    }
                    break;
                case MenuPrestamos.Salir:
                    GestionarMenuUsuario(usuario);
                    break;
            }
        }

        public static void GestionarMenuPagos(Usuario usuario)
        {
            Console.ReadKey();
            Console.Clear();
            var option = AnsiConsole.Prompt(
            new SelectionPrompt<MenuPagos>()
            .Title("Te encuentras en el menu de Pagos, ¿A que tipo de grupo deseas pagarle? ")
            .AddChoices(
                MenuPagos.PagarMisPrestamos,
                MenuPagos.Salir));

            switch (option)
            {
                case MenuPagos.PagarMisPrestamos:
                    TransaccionesGrupoAhorroBD.RegistrarPagoPrestamo(usuario);
                    break;        
                case MenuPagos.Salir:
                    GestionarMenuUsuario(usuario);
                    break;
            }
        }
    }
}
