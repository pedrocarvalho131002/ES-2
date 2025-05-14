using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

public class CriarProdutoModel : PageModel
{
    [BindProperty] public ProdutoViewModel Produto { get; set; } = new();
    public List<SelectListItem> Categorias { get; set; } = new();
    public string? Erro { get; set; }

    public async Task OnGetAsync() => await CarregarCategorias();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await CarregarCategorias();
            return Page();
        }

        using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
        var response = await client.PostAsJsonAsync("/api/produto", Produto);

        if (response.IsSuccessStatusCode) return RedirectToPage("/MainUtilizador");

        Erro = $"Erro ao criar produto. Código: {response.StatusCode}";
        await CarregarCategorias();
        return Page();
    }

    private async Task CarregarCategorias()
    {
        using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
        var categorias = await client.GetFromJsonAsync<List<CategoriaViewModel>>("/api/categoria");

        Categorias = categorias.Select(c => new SelectListItem { Value = c.CategoriaId.ToString(), Text = c.Nome }).ToList();
    }
}
