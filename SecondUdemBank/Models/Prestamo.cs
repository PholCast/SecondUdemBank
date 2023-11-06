using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace SecondUdemBank
{
    public class Prestamo
    {
        [Key]
        public int id { get; set; }
        public int id_usuarioXGrupoDeAhorro { get; set; }
        [ForeignKey(nameof(id_usuarioXGrupoDeAhorro))]
        public UsuarioXGrupoAhorro usuarioXGrupoDeAhorro { get; set; }

        [Required]
        public double cantidadPrestamo { get; set; } //Lo que prestó

        [Required]
        public double deudaActual {  get; set; }

        [Required]
        public double cantidadCuota { get; set; }

        [Required]
        public double cantidadAPagar {  get; set; } //Lo que deberia pagar teniendo en cuenta intereses

        [Required]
        public DateOnly fechaPrestamo { get; set; }

        [Required]
        public DateOnly fechaPlazo { get; set; }

        [Required]
        public double interes {  get; set; }
    }
}
