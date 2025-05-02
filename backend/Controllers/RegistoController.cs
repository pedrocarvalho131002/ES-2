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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verifica se o produto já existe (por nome e marca)
            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Nome == model.Produto.Nome && p.Marca == model.Produto.Marca);

            // Se não existir, cria novo produto (sem definir manualmente o ID)
            if (produto == null)
            {
                produto = new Produto
                {
                    Nome = model.Produto.Nome,
                    Marca = model.Produto.Marca,
                    Descricao = model.Produto.Descricao,
                    CategoriaId = model.Produto.CategoriaId
                };

                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync(); // produto.ProdutoId é gerado automaticamente
            }

            // Cria o registo de preço
            var registo = new RegistoPreco
            {
                Preco = model.Preco,
                DataRegisto = DateTime.UtcNow,
                Credibilidade = 1.0, // valor base
                TipoAcaoId = model.TipoAcaoId,
                ProdutoId = produto.ProdutoId,
                LojaId = model.LojaId,
                UtilizadorId = model.UtilizadorId
            };

            _context.RegistosPrecos.Add(registo);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registo de preço efetuado com sucesso." });
        }
    }
}
