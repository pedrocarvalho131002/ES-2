using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels; // ou ajusta conforme o namespace
using System.Net.Http.Json;

public class RegistarModel : PageModel
{
    [BindProperty]
    public UtilizadorViewModel Utilizador { get; set; }

    public string? Erro { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Erro = "Preenche todos os campos corretamente.";
            return Page();
        }

        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5000");

        var response = await client.PostAsJsonAsync("/api/auth/registar", Utilizador);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("/Login");
        }

        // Mostra uma mensagem mais clara ao utilizador
        Erro = "Erro ao registar utilizador. Verifica se o nome de utilizador já existe ou se os dados estão corretos.";
        return Page();
    }
}
