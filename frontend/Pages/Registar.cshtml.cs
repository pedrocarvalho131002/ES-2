using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels; // ou ajusta conforme o namespace

public class RegistarModel : PageModel
{
    [BindProperty]
    public UtilizadorViewModel Utilizador { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5000");

        var response = await client.PostAsJsonAsync("/api/auth/registar", Utilizador);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("/Login");
        }

        ModelState.AddModelError(string.Empty, "Erro ao registar utilizador.");
        return Page();
    }
}
