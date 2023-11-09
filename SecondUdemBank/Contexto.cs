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
        public virtual DbSet<Usuario>Usuarios{ get; set; }
        public virtual DbSet<udemBank> udemBanks {  get; set; }
        public virtual DbSet<CuentaDeAhorro> CuentasDeAhorros { get; set; }

        public virtual DbSet<GrupoDeAhorro> GruposDeAhorros { get; set; }

        public virtual DbSet<TransaccionesGrupoAhorro> TransaccionesGruposAhorros { get; set; }
        public virtual DbSet<Transacciones> TransaccionesCuentaAhorros { get; set; }

        public virtual DbSet<Prestamo> Prestamos {  get; set; }

        public virtual DbSet<UsuarioXGrupoAhorro> UsuariosXGruposAhorros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = SecondUdemBank.db");
        }

    }
}
