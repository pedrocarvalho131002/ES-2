using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPrecos.API.Models
{
    [Table("tipoacao")]
    public class TipoAcao
    {
        [Key]
        [Column("tipo_acao_id")]
        public int TipoAcaoId { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }
    }
}
