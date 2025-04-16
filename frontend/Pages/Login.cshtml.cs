using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SistemaPrecos.Web.ViewModels;
using System.Net.Http.Json;

namespace SistemaPrecos.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string? Erro { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("OnPostAsync chamado!");

            var client = _httpClientFactory.CreateClient("Api");

            var login = new LoginRequest
            {
                Username = Username,
                Password = Password
            };

            var response = await client.PostAsJsonAsync("auth/login", login);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                HttpContext.Response.Cookies.Append("username", result!.Nome);
                HttpContext.Response.Cookies.Append("tipo", result.Tipo);

                return result.Tipo == "Administrador"
                    ? RedirectToPage("/MainAdmin")
                    : RedirectToPage("/MainUtilizador");
            }

            // DEBUG EXTRA: Mostra o erro de forma mais clara
            var errorDetails = await response.Content.ReadAsStringAsync();
            Erro = $"Erro ao iniciar sessão. Código: {response.StatusCode}. Detalhes: {errorDetails}";
            return Page();
        }
    }
}
