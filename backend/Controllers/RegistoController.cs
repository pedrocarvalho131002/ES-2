using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPrecos.API.Data;
using SistemaPrecos.API.Models;
using SistemaPrecos.API.ViewModels;

namespace SistemaPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistoPrecoController : ControllerBase
    {
        private readonly SistemaPrecosContext _context;

        public RegistoPrecoController(SistemaPrecosContext context)
        {
            _context = context;
        }

    
        [HttpPost]
        public async Task<IActionResult> RegistarPreco([FromBody] RegistoPrecoViewModel model)
        {
            Console.WriteLine("📥 Pedido recebido em /api/registopreco");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model.Preco <= 0)
                return BadRequest("Preço deve ser superior a zero.");

            var produto = await _context.Produtos
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(p => p.ProdutoId == model.ProdutoId);

            if (produto is null)
                return BadRequest("ProdutoId inválido.");

            /* ---------- verifica se loja existe ----------------------------- */
            var lojaExiste = await _context.Lojas
                                           .AnyAsync(l => l.LojaId == model.LojaId);

            if (!lojaExiste)
                return BadRequest("LojaId inválido.");

            /* ---------- cria registo de preço ------------------------------- */
            var registo = new RegistoPreco
            {
                Preco        = model.Preco,
                DataRegisto  = DateTime.UtcNow,
                Credibilidade = 1.0,          // valor base
                TipoAcaoId   = model.TipoAcaoId,
                ProdutoId    = model.ProdutoId,
                LojaId       = model.LojaId,
                UtilizadorId = model.UtilizadorId
            };

            _context.RegistoPrecos.Add(registo);
            await _context.SaveChangesAsync();

            Console.WriteLine($"✅ Registo de preço inserido (ID {registo.RegistoPrecoId}).");

            return CreatedAtAction(nameof(RegistarPreco),   // ou um GET se tiveres
                                   new { id = registo.RegistoPrecoId },
                                   new { message = "Registo de preço efetuado com sucesso." });
        }
    }
}
