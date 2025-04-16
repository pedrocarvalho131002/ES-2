using Microsoft.AspNetCore.Mvc;
using SistemaPrecos.API.Models;
using SistemaPrecos.API.Repositories;

namespace SistemaPrecos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepository _repo;

        public CategoriaController(CategoriaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            var categorias = _repo.GetAll(); // ou outro nome, depende do teu repo
            return Ok(categorias);
        }
    }
}
