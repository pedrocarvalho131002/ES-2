using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPrecos.API.Models
{
    [Table("registosprecos")]
    public class RegistoPreco
    {
        [Key]
        [Column("registo_preco_id")]
        public int RegistoPrecoId { get; set; }

        [Column("preco")]
        public decimal Preco { get; set; }

        [Column("dataregisto")]
        public DateTime DataRegisto { get; set; }

        [Column("credibilidade")]
        public double Credibilidade { get; set; }

        [Column("tipo_acao_id")]
        public int TipoAcaoId { get; set; }

        [Column("produto_id")]
        public int ProdutoId { get; set; }

        [Column("loja_id")]
        public int LojaId { get; set; }

        [Column("utilizador_id")]
        public int UtilizadorId { get; set; }
    }
}
