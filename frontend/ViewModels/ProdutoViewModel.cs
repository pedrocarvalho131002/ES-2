namespace SistemaPrecos.Web.ViewModels
{
    public class ProdutoViewModel
    {
        public int    ProdutoId   { get; set; }
        public string Nome        { get; set; } = default!;
        public string Marca       { get; set; } = default!;
        public string Descricao   { get; set; } = default!;
        public int    CategoriaId { get; set; }
    }
}
