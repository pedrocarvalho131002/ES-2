namespace SistemaPrecos.Web.ViewModels
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Descricao { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }

    public class Categoria
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
    }
}