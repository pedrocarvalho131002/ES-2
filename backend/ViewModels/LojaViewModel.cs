namespace SistemaPrecos.API.ViewModels
{
    public class LojaViewModel
    {
        public int LojaId { get; set; }
        public string Nome { get; set; } = string.Empty;

        public LocalizacaoViewModel Localizacao { get; set; } = new();
    }

    public class LocalizacaoViewModel
    {
        public int LocalizacaoId { get; set; }
        public string Cidade { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
