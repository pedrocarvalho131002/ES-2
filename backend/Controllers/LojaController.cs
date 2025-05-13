using Microsoft.AspNetCore.Mvc;
using SistemaPrecos.API.Models;
using SistemaPrecos.API.Repositories;

namespace SistemaPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LojaController : ControllerBase
    {
        private readonly LojaRepository _repo;

        public LojaController(LojaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Loja>> GetLojas()
        {
            var lojas = _repo.GetAll();
            return Ok(lojas);
        }
    }
}
