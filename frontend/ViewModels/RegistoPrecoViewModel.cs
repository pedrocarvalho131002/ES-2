namespace SistemaPrecos.Web.ViewModels
{
    public class RegistoPrecoViewModel
    {
        public int ProdutoId    { get; set; }
        public int LojaId       { get; set; }
        public decimal Preco    { get; set; }
        public int UtilizadorId { get; set; }
        public int TipoAcaoId   { get; set; }
    }
}
