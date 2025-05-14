using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

namespace SistemaPrecos.Web.Pages
{
    public class MainUtilizadorModel : PageModel
    {
        public string NomeUtilizador { get; set; } = string.Empty;
        public List<MensagemViewModel> Mensagens { get; set; } = new();

        public async Task OnGetAsync()
        {
            NomeUtilizador = Request.Cookies["username"] ?? "Utilizador";

            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };

            var resultado = await client.GetFromJsonAsync<List<MensagemViewModel>>("/api/mensagem");
            if (resultado != null)
                Mensagens = resultado.OrderByDescending(m => m.DataEnvio).ToList();
        }
    }
}
