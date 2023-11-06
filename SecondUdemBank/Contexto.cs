using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Microsoft.EntityFrameworkCore;

namespace SecondUdemBank
{
    public class Contexto: DbContext
    {
        public DbSet<udemBank> udemBanks {  get; set; }
        public DbSet<CuentaDeAhorro> CuentasDeAhorros { get; set; }

        public DbSet<GrupoDeAhorro> GruposDeAhorros { get; set; }

        public DbSet<TransaccionesGrupoAhorro> TransaccionesGruposAhorros { get; set; }
        public DbSet<Transacciones> TransaccionesCuentaAhorros { get; set; }

        public DbSet<Prestamo> Prestamos {  get; set; }
        public DbSet<Usuario>Usuarios{ get; set; }

        public DbSet<UsuarioXGrupoAhorro> UsuariosXGruposAhorros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = SecondUdemBank.db");
        }

    }
}
