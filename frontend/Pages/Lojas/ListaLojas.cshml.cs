using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

namespace SistemaPrecos.Web.Pages.Admin.Lojas
{
    public class ListaLojasModel : PageModel
    {
        public List<LojaViewModel> Lojas { get; set; } = new();

        public async Task OnGetAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");

            var lojas = await client.GetFromJsonAsync<List<LojaViewModel>>("/api/loja");
            if (lojas != null)
                Lojas = lojas;
        }
    }
}
