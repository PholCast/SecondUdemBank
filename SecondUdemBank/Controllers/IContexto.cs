using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondUdemBank
{
    public interface IContexto
    {
        public DbSet<udemBank> udemBanks { get; set; }
        public DbSet<CuentaDeAhorro> CuentasDeAhorros { get; set; }


        public DbSet<GrupoDeAhorro> GruposDeAhorros { get; set; }

        public DbSet<TransaccionesGrupoAhorro> TransaccionesGruposAhorros { get; set; }
        public DbSet<Transacciones> TransaccionesCuentaAhorros { get; set; }

        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<UsuarioXGrupoAhorro> UsuariosXGruposAhorros { get; set; }
        // Agrega las demás propiedades DbSet aquí
    }

}
