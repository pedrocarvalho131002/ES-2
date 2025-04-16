using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPrecos.API.Models
{
    [Table("localizacao")]
    public class Localizacao
    {
        [Key]
        [Column("localizacao_id")]
        public int LocalizacaoId { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("pais")]
        public string Pais { get; set; }

        [Column("codigo_postal")]
        public string CodigoPostal { get; set; }

        [Column("latitude")]
        public double Latitude { get; set; }

        [Column("longitude")]
        public double Longitude { get; set; }
    }
}
