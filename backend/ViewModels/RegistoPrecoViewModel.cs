using System.ComponentModel.DataAnnotations;

namespace SistemaPrecos.API.ViewModels
{
    public class RegistoPrecoViewModel
    {
        [Required]
        public int ProdutoId   { get; set; }

        [Required]
        public int LojaId      { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser superior a 0")]
        public decimal Preco    { get; set; }

        [Required]
        public int UtilizadorId { get; set; }

        public int TipoAcaoId   { get; set; } = 1;
    }
}
