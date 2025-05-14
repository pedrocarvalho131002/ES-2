using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

public class RegistarPrecoModel : PageModel
{
    // 👇 Agora só precisamos do ID do produto
    [BindProperty] public int ProdutoId { get; set; }
    [BindProperty] public int LojaId    { get; set; }
    [BindProperty] public decimal Preco { get; set; }

    public List<SelectListItem> Produtos { get; set; } = new();
    public List<SelectListItem> Lojas    { get; set; } = new();

    public string? Erro { get; set; }

    public async Task OnGetAsync() => await CarregarProdutosELojas();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!Request.Cookies.TryGetValue("userid", out var idStr) || !int.TryParse(idStr, out var utilizadorId) || utilizadorId <= 0)
        {
            Erro = "Utilizador não autenticado.";
            await CarregarProdutosELojas();
            return Page();
        }

        using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };

        var request = new RegistoPrecoViewModel
        {
            ProdutoId   = ProdutoId,
            LojaId      = LojaId,
            Preco       = Preco,
            UtilizadorId = utilizadorId,
            TipoAcaoId  = 1   // 1 = inserção (mantivemos o seu código original)
        };

        var response = await client.PostAsJsonAsync("/api/registopreco", request);
        if (response.IsSuccessStatusCode) return RedirectToPage("/MainUtilizador");

        var detalhes = await response.Content.ReadAsStringAsync();
        Erro = $"Erro ao registar o preço. Código: {response.StatusCode}. Detalhes: {detalhes}";

        await CarregarProdutosELojas();
        return Page();
    }

    private async Task CarregarProdutosELojas()
    {
        using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };

        var produtos = await client.GetFromJsonAsync<List<ProdutoViewModel>>("/api/produto");
        var lojas    = await client.GetFromJsonAsync<List<LojaViewModel>>   ("/api/loja");

        Produtos = produtos.Select(p => new SelectListItem { Value = p.ProdutoId.ToString(), Text = $"{p.Marca} {p.Nome}" }).ToList();
        Lojas    = lojas   .Select(l => new SelectListItem { Value = l.LojaId.ToString(),    Text = l.Nome }).ToList();
    }
}
