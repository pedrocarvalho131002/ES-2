using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SistemaPrecos.Web.Pages
{
    public class MainAdminModel : PageModel
    {
        public string NomeUtilizador { get; set; } = "Administrador";

        public void OnGet()
        {
            if (Request.Cookies.ContainsKey("username"))
            {
                NomeUtilizador = Request.Cookies["username"]!;
            }
        }
    }
}
