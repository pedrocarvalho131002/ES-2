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
                Tipo = utilizador.TipoUtilizador.Tipo, // "Administrador" ou "Utilizador"
                UtilizadorId = utilizador.UtilizadorId 
            });
        }

        [HttpPost("registar")]
        public async Task<IActionResult> Registar([FromBody] UtilizadorCreateViewModel novoUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar se já existe utilizador com o mesmo username ou email
            var existente = await _context.Utilizadores
                .AnyAsync(u => u.Username == novoUser.Username || u.Email == novoUser.Email);

            if (existente)
                return Conflict("Já existe um utilizador com esse username ou email.");

            var utilizador = new Utilizador
            {
                Nome = novoUser.Nome,
                Username = novoUser.Username,
                Email = novoUser.Email,
                Password = novoUser.Password,
                TipoUtilizadorId = 2 // Tipo 'Utilizador' por defeito
            };

            _context.Utilizadores.Add(utilizador);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Utilizador registado com sucesso." });
        }
    }
}
