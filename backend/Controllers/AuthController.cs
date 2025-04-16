using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPrecos.API.Data;
using SistemaPrecos.API.Models;
using SistemaPrecos.API.ViewModels;

namespace SistemaPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SistemaPrecosContext _context;

        public AuthController(SistemaPrecosContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var utilizador = _context.Utilizadores
                .Include(u => u.TipoUtilizador)
                .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

            if (utilizador == null)
            {
                return Unauthorized();
            }

            return Ok(new LoginResponse
            {
                Nome = utilizador.Nome,
                Tipo = utilizador.TipoUtilizador.Tipo // "Administrador" ou "Utilizador"
            });
        }
    }
}
