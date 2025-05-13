using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

public class RegistarPrecoModel : PageModel
{
    [BindProperty] public int ProdutoId { get; set; }
    [BindProperty] public int LojaId    { get; set; }
    [BindProperty] public decimal Preco { get; set; }

    public List<SelectListItem> Produtos { get; set; } = new();
    public List<SelectListItem> Lojas    { get; set; } = new();

    public string? Erro { get; set; }

    public async Task OnGetAsync() => await CarregarListas();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!int.TryParse(Request.Cookies["userid"], out int utilizadorId) || utilizadorId <= 0)
        {
            Erro = "Utilizador não autenticado.";
            await CarregarListas();
            return Page();
        }

        var req = new RegistoPrecoViewModel
        {
            ProdutoId    = ProdutoId,
            LojaId       = LojaId,
            Preco        = Preco,
            UtilizadorId = utilizadorId,
            TipoAcaoId   = 1
        };

        using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
        var resp = await client.PostAsJsonAsync("/api/registopreco", req);

        if (resp.IsSuccessStatusCode) return RedirectToPage("/MainUtilizador");

        Erro = $"Erro: {(int)resp.StatusCode}";
        await CarregarListas();
        return Page();
    }

    private async Task CarregarListas()
    {
        using var c = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };

        var produtos = await c.GetFromJsonAsync<List<ProdutoViewModel>>("/api/produto");
        var lojas    = await c.GetFromJsonAsync<List<LojaViewModel>>   ("/api/loja");

        Produtos = produtos!
            .Select(p => new SelectListItem { Value = p.ProdutoId.ToString(), Text = p.Nome })
            .OrderBy(p => p.Text)
            .ToList();

        Lojas = lojas!
            .Select(l => new SelectListItem { Value = l.LojaId.ToString(), Text = l.Nome })
            .OrderBy(l => l.Text)
            .ToList();
    }
}
