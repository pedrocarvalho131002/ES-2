using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;


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

    public async Task OnGetAsync()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5000");

        var categorias = await client.GetFromJsonAsync<List<CategoriaViewModel>>("/api/categoria");
        var lojas = await client.GetFromJsonAsync<List<LojaViewModel>>("/api/loja");

        Categorias = categorias.Select(c => new SelectListItem { Value = c.CategoriaId.ToString(), Text = c.Nome }).ToList();
        Lojas = lojas.Select(l => new SelectListItem { Value = l.LojaId.ToString(), Text = l.Nome }).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5000");

        var request = new RegistoPrecoViewModel
        {
            Produto = Produto,
            LojaId = LojaId,
            Preco = Preco,
            UtilizadorId = 1, // ← por agora hardcoded
            TipoAcaoId = 1 // Adicionar
        };

        var response = await client.PostAsJsonAsync("/api/registopreco", request);

        if (response.IsSuccessStatusCode)
            return RedirectToPage("/MainUtilizador");

        return Page();
    }
}
