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
            Credibilidade = 1
        };

        _ctx.RegistoPrecos.Add(reg);
        await _ctx.SaveChangesAsync();
        return Ok();
    }
}
