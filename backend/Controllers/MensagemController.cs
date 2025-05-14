using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPrecos.API.Data;
using SistemaPrecos.API.Models;

namespace SistemaPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MensagemController : ControllerBase
    {
        private readonly SistemaPrecosContext _context;

        public MensagemController(SistemaPrecosContext context)
        {
            _context = context;
        }

        // POST: api/mensagem/enviar
        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarMensagem([FromBody] Mensagem mensagem)
        {
            if (string.IsNullOrWhiteSpace(mensagem.Conteudo))
                return BadRequest("A mensagem não pode estar vazia.");

            mensagem.DataEnvio = DateTime.UtcNow;

            _context.Mensagens.Add(mensagem);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Mensagem enviada com sucesso." });
        }

        // GET: api/mensagem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mensagem>>> GetMensagens()
        {
            var mensagens = await _context.Mensagens
                .OrderByDescending(m => m.DataEnvio)
                .ToListAsync();

            return Ok(mensagens);
        }

        // GET: api/mensagem/utilizador/5
        [HttpGet("utilizador/{id}")]
        public async Task<ActionResult<IEnumerable<Mensagem>>> GetMensagensPorUtilizador(int id)
        {
            var mensagens = await _context.Mensagens
                .Where(m => m.IdUtilizador == id)
                .OrderByDescending(m => m.DataEnvio)
                .ToListAsync();

            return Ok(mensagens);
        }
    }
}
