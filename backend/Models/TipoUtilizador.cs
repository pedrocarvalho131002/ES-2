using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPrecos.API.Models
{
    [Table("tipoutilizador")]
    public class TipoUtilizador
    {
        [Key]
        [Column("tipo_utilizador_id")]
        public int TipoUtilizadorId { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }
    }
}
