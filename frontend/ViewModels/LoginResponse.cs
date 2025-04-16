namespace SistemaPrecos.Web.ViewModels
{
    public class LoginResponse
    {
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // ex: "Administrador" ou "Utilizador"
    }
}
