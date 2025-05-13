using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPrecos.API.Models
{
    [Table("categorias")]
    public class Categoria
    {
        [Key]
        [Column("categoria_id")]
        public int CategoriaId { get; set; }

        [Column("nome")]
        public string Nome { get; set; }
    }
}
