namespace SistemaPrecos.API.ViewModels
{
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }
        public string Nome   { get; set; } = default!;
        public string Marca { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
    }
}