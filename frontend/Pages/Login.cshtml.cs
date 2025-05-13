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
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Erro = "Preenche todos os campos.";
                return Page();
            }

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

                // Guardar cookies
                HttpContext.Response.Cookies.Append("username", result!.Nome);
                HttpContext.Response.Cookies.Append("tipo", result.Tipo);
                HttpContext.Response.Cookies.Append("userid", result.UtilizadorId.ToString());

                // Redirecionar com base no tipo
                return result.Tipo == "Administrador"
                    ? RedirectToPage("/MainAdmin")
                    : RedirectToPage("/MainUtilizador");
            }

            Erro = "Credenciais inválidas. Verifica o nome de utilizador e a password.";
            return Page();
        }
    }
}
