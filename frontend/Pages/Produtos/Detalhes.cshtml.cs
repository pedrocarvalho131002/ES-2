using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

namespace SistemaPrecos.Web.Pages.Produtos
{
    public class DetalhesModel : PageModel
    {
        private readonly HttpClient _http;

        public DetalhesModel(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("Api");
        }

        public Produto Produto { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _http.GetFromJsonAsync<Produto>($"produto/{id}");

            if (result == null)
                return NotFound();

            Produto = result;
            return Page();
        }
    }
}
