using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPrecos.API.Data;
using SistemaPrecos.API.Models;
using SistemaPrecos.API.ViewModels;

[ApiController]
[Route("api/registopreco")]
public class RegistoController : ControllerBase
{
    private readonly SistemaPrecosContext _ctx;
    public RegistoController(SistemaPrecosContext ctx) => _ctx = ctx;

    [HttpPost]
    public async Task<IActionResult> Post(RegistoPrecoViewModel vm)
    {
        // ✅ valida se existem PKs recebidas
        if (!await _ctx.Produtos.AnyAsync(p => p.ProdutoId == vm.ProdutoId))
            return BadRequest(new { Erro = "Produto inexistente" });

        if (!await _ctx.Lojas.AnyAsync(l => l.LojaId == vm.LojaId))
            return BadRequest(new { Erro = "Loja inexistente" });

        var reg = new RegistoPreco
        {
            ProdutoId    = vm.ProdutoId,
            LojaId       = vm.LojaId,
            Preco        = vm.Preco,
            UtilizadorId = vm.UtilizadorId,
            TipoAcaoId   = vm.TipoAcaoId,
            DataRegisto  = DateTime.UtcNow,
            Credibilidade = 5
        };

        _ctx.RegistoPrecos.Add(reg);
        await _ctx.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("produto/{produtoId:int}")]
    public async Task<ActionResult<IEnumerable<PrecoLinhaVM>>> PorProduto(int produtoId)
    {
        // 1) busca as colunas necessárias
        var dados = await
            (from r   in _ctx.RegistoPrecos
            join l   in _ctx.Lojas          on r.LojaId        equals l.LojaId
            join loc in _ctx.Localizacoes   on l.LocalizacaoId equals loc.LocalizacaoId
            where r.ProdutoId == produtoId
            orderby r.DataRegisto descending
            select new
            {
                l.Nome,
                Localizacao = loc.Cidade + " (" + loc.CodigoPostal + ")",
                r.Preco,
                r.DataRegisto
            })
            .ToListAsync();

        // 2) calcula credibilidade “on-the-fly”
        var agora = DateTime.UtcNow;
        const double decaimentoDia = 0.02;          // 0,02/dia  ⇒ 0 em 50 dias

        var result = dados.Select(x =>
        {
            var dias = (agora - x.DataRegisto).TotalDays;
            var cred = Math.Max(0, 1 - dias * decaimentoDia);

            return new PrecoLinhaVM(
                Loja:         x.Nome,
                Localizacao:  x.Localizacao,
                Preco:        x.Preco,
                DataRegisto:  x.DataRegisto,
                Credibilidade: cred
            );
        });

        return Ok(result);
    }

    public record PrecoLinhaVM(
        string  Loja,
        string  Localizacao,
        decimal Preco,
        DateTime DataRegisto,
        double  Credibilidade);

}