using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPrecos.API.Models
{
    [Table("lojas")]
    public class Loja
    {
        [Key]
        [Column("loja_id")]
        public int LojaId { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("localizacao_id")]
        public int LocalizacaoId { get; set; }

        // 🆕 Propriedade de navegação
        public Localizacao Localizacao { get; set; }
    }
}
