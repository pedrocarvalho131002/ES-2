using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

namespace SistemaPrecos.Web.Pages.Categorias
{
    public class ListaCategoriasModel : PageModel
    {
        public List<CategoriaViewModel> Categorias { get; set; } = new();

        public async Task OnGetAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000");

            var categorias = await client.GetFromJsonAsync<List<CategoriaViewModel>>("/api/categoria");
            if (categorias != null)
                Categorias = categorias;
        }
    }
}
