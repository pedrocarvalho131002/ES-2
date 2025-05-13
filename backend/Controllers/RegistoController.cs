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

            try
            {
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("❌ ModelState inválido.");
                    return BadRequest(ModelState);
                }

                // Log do conteúdo recebido
                Console.WriteLine("📝 Dados recebidos:");
                Console.WriteLine($"Produto: {model.Produto?.Nome}, {model.Produto?.Marca}, {model.Produto?.Descricao}");
                Console.WriteLine($"CategoriaId: {model.Produto?.CategoriaId}");
                Console.WriteLine($"LojaId: {model.LojaId}, Preco: {model.Preco}, UtilizadorId: {model.UtilizadorId}, TipoAcaoId: {model.TipoAcaoId}");

                // Verifica se o produto já existe (por nome e marca)
                var produto = await _context.Produtos
                    .FirstOrDefaultAsync(p => p.Nome == model.Produto.Nome && p.Marca == model.Produto.Marca);

                // Se não existir, cria novo produto
                if (produto == null)
                {
                    Console.WriteLine("➕ Produto novo — será criado.");
                    produto = new Produto
                    {
                        Nome = model.Produto.Nome,
                        Marca = model.Produto.Marca,
                        Descricao = model.Produto.Descricao,
                        CategoriaId = model.Produto.CategoriaId
                    };

                    _context.Produtos.Add(produto);
                    await _context.SaveChangesAsync(); // Gera ProdutoId
                    Console.WriteLine($"✅ Produto criado com ID {produto.ProdutoId}");
                }
                else
                {
                    Console.WriteLine($"ℹ️ Produto já existente com ID {produto.ProdutoId}");
                }

                // Verifica se a loja existe
                var lojaExiste = await _context.Lojas.AnyAsync(l => l.LojaId == model.LojaId);
                if (!lojaExiste)
                {
                    Console.WriteLine("❌ Loja não encontrada.");
                    return BadRequest("LojaId inválido.");
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

                Console.WriteLine("✅ Registo de preço guardado com sucesso.");
                return Ok(new { message = "Registo de preço efetuado com sucesso." });
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ ERRO AO REGISTAR PREÇO:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
