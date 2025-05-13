using SistemaPrecos.API.Data;
using SistemaPrecos.API.Models;

namespace SistemaPrecos.API.Repositories
{
    public class CategoriaRepository
    {
        private readonly SistemaPrecosContext _context;

        public CategoriaRepository(SistemaPrecosContext context)
        {
            _context = context;
        }

        public List<Categoria> GetAll()
        {
            return _context.Categorias.ToList();
        }
    }
}
