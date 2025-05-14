namespace SistemaPrecos.Web.ViewModels
{
    public class MensagemViewModel
    {
        public int IdMensagem { get; set; }
        public string Conteudo { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; }
        public int IdUtilizador { get; set; }
    }
}
