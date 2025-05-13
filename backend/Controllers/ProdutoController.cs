using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPrecos.API.Data;
using SistemaPrecos.API.Models;
using SistemaPrecos.API.ViewModels;

namespace SistemaPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly SistemaPrecosContext _context;

        public ProdutoController(SistemaPrecosContext context)
        {
            _context = context;
        }

        /* -------------------------------------------------------------------
         * GET: /api/produto
         * -------------------------------------------------------------------*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        /* -------------------------------------------------------------------
         * GET: /api/produto/{id}
         * -------------------------------------------------------------------*/
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            return produto is null ? NotFound() : produto;
        }

        /* -------------------------------------------------------------------
         * POST: /api/produto
         * Cria um novo produto (NÃO regista preço)
         * -------------------------------------------------------------------*/
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            // validações simples de exemplo
            if (string.IsNullOrWhiteSpace(produto.Nome))
                return BadRequest("Nome é obrigatório.");

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            // devolve 201 Created e cabeçalho Location
            return CreatedAtAction(
                nameof(GetProduto),
                new { id = produto.ProdutoId },
                produto);
        }

        /* -------------------------------------------------------------------
         * GET: /api/produto/{id}/comparar
         * Devolve o preço mais recente do produto em cada loja
         * -------------------------------------------------------------------*/
        [HttpGet("{id}/comparar")]
        public async Task<ActionResult<IEnumerable<ComparacaoPrecoViewModel>>> CompararPrecos(int id)
        {
            var precos = await _context.RegistoPrecos
                .Where(r => r.ProdutoId == id)
                .GroupBy(r => r.LojaId)
                .Select(g => g.OrderByDescending(r => r.DataRegisto).First())
                .ToListAsync();

            var lojas = await _context.Lojas
                .Include(l => l.Localizacao)
                .ToListAsync();

            var resultado = precos.Join(
                    lojas,
                    preco => preco.LojaId,
                    loja  => loja.LojaId,
                    (preco, loja) => new ComparacaoPrecoViewModel
                    {
                        NomeLoja    = loja.Nome,
                        Localizacao = $"{loja.Localizacao.Cidade} ({loja.Localizacao.CodigoPostal})",
                        Preco       = preco.Preco,
                        Data        = preco.DataRegisto
                    })
                .ToList();

            return Ok(resultado);
        }
    }
}
