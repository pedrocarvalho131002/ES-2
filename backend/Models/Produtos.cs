using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPrecos.API.Models
{
    [Table("produtos")]
    public class Produto
    {
        [Key]
        [Column("produto_id")]
        public int ProdutoId { get; set; }

        [Column("marca")]
        public string Marca { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("categoria_id")]
        public int CategoriaId { get; set; }
    }
}
