using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

public class CriarLojaModel : PageModel
{
    [BindProperty] public LojaViewModel Loja { get; set; } = new();
    public string? Erro { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
        var resposta = await client.PostAsJsonAsync("/api/loja", Loja);

        if (resposta.IsSuccessStatusCode)
            return RedirectToPage("/MainUtilizador");

        Erro = $"Erro ao criar loja. Código: {resposta.StatusCode}";
        return Page();
    }
}
