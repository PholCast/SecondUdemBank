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
    public class UsuarioXGrupoAhorro
    {
        [Key]
        public int id { get; set; }
        public int id_ParticipanteGrupo { get; set; }
        [ForeignKey(nameof(id_ParticipanteGrupo))]
        public Usuario Usuario { get; set; }
        public int id_GrupoAhorro { get; set; }
        [ForeignKey(nameof(id_GrupoAhorro))]
        public GrupoDeAhorro GrupoDeAhorro { get; set; }

        [Required]
        public bool PerteneceAlGrupo { get; set; }
    }
}
