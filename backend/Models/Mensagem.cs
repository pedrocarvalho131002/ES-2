using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaPrecos.API.Models
{
    [Table("mensagem")]
    public class Mensagem
    {
        [Key]
        [Column("id_mensagem")]
        public int IdMensagem { get; set; }

        [Column("conteudo")]
        public string Conteudo { get; set; }

        [Column("data_envio")]
        public DateTime DataEnvio { get; set; }

        [Column("id_utilizador")]
        public int IdUtilizador { get; set; }
    }
}
