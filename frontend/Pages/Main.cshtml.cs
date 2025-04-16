using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaPrecos.Web.Pages
{
    public class MainModel : PageModel
    {
        public bool EstaAutenticado { get; set; } = false;
        public string NomeUtilizador { get; set; } = "Visitante";

        public void OnGet()
        {
            // Verifica se está autenticado (ex: via cookie ou sessão)
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                EstaAutenticado = true;
                NomeUtilizador = User.Identity.Name ?? "Utilizador";
            }
        }
    }
}
