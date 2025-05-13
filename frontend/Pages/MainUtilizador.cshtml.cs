using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaPrecos.Web.Pages
{
    public class MainUtilizadorModel : PageModel
    {
        public string NomeUtilizador { get; set; } = "Utilizador";

        public void OnGet()
        {
            if (Request.Cookies.ContainsKey("username"))
            {
                NomeUtilizador = Request.Cookies["username"]!;
            }
        }
    }

}
