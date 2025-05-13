using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

public class RegistarPrecoModel : PageModel
{
    [BindProperty]
    public ProdutoViewModel Produto { get; set; } = new();

    [BindProperty]
    public int LojaId { get; set; }

    [BindProperty]
    public decimal Preco { get; set; }

    public List<SelectListItem> Categorias { get; set; } = new();
    public List<SelectListItem> Lojas { get; set; } = new();

    public string? Erro { get; set; }

    public async Task OnGetAsync()
    {
        await CarregarCategoriasELojas();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!Request.Cookies.ContainsKey("userid"))
        {
            Erro = "Utilizador não autenticado.";
            await CarregarCategoriasELojas();
            return Page();
        }

        if (!int.TryParse(Request.Cookies["userid"], out int utilizadorId))
        {
            Erro = "Erro ao identificar o utilizador.";
            await CarregarCategoriasELojas();
            return Page();
        }

        if (utilizadorId <= 0)
        {
            Erro = "Utilizador não autenticado corretamente. Por favor faça login novamente.";
            await CarregarCategoriasELojas();
            return Page();
        }

        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5000");

        var request = new RegistoPrecoViewModel
        {
            Produto = Produto,
            LojaId = LojaId,
            Preco = Preco,
            UtilizadorId = utilizadorId,
            TipoAcaoId = 1
        };

        var response = await client.PostAsJsonAsync("/api/registopreco", request);

        if (response.IsSuccessStatusCode)
            return RedirectToPage("/MainUtilizador");

        var detalhes = await response.Content.ReadAsStringAsync();
        Erro = $"Erro ao registar o produto. Código: {response.StatusCode}. Detalhes: {detalhes}";

        Console.WriteLine("Cookie userid: " + Request.Cookies["userid"]);
        await CarregarCategoriasELojas();
        return Page();
    }

    private async Task CarregarCategoriasELojas()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5000");

        var categorias = await client.GetFromJsonAsync<List<CategoriaViewModel>>("/api/categoria");
        var lojas = await client.GetFromJsonAsync<List<LojaViewModel>>("/api/loja");

        Categorias = categorias.Select(c => new SelectListItem
        {
            Value = c.CategoriaId.ToString(),
            Text = c.Nome
        }).ToList();

        Lojas = lojas.Select(l => new SelectListItem
        {
            Value = l.LojaId.ToString(),
            Text = l.Nome
        }).ToList();
    }
}
