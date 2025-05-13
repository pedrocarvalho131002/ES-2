using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPrecos.API.Data;
using SistemaPrecos.API.Repositories;
using SistemaPrecos.API.ViewModels;

namespace SistemaPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LojaController : ControllerBase
    {
        private readonly SistemaPrecosContext _context;

        public LojaController(SistemaPrecosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LojaViewModel>>> GetLojas()
        {
            var lojas = await _context.Lojas
                .Include(l => l.Localizacao)
                .Select(l => new LojaViewModel
                {
                    LojaId = l.LojaId,
                    Nome = l.Nome,
                    Localizacao = new LocalizacaoViewModel
                    {
                        LocalizacaoId = l.Localizacao.LocalizacaoId,
                        Cidade = l.Localizacao.Cidade,
                        Pais = l.Localizacao.Pais,
                        CodigoPostal = l.Localizacao.CodigoPostal,
                        Latitude = l.Localizacao.Latitude,
                        Longitude = l.Localizacao.Longitude
                    }
                })
                .ToListAsync();

            return Ok(lojas);
        }
    }
}
