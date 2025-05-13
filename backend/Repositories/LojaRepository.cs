using SistemaPrecos.API.Data;
using SistemaPrecos.API.Models;

namespace SistemaPrecos.API.Repositories
{
    public class LojaRepository
    {
        private readonly SistemaPrecosContext _context;

        public LojaRepository(SistemaPrecosContext context)
        {
            _context = context;
        }

        public List<Loja> GetAll()
        {
            return _context.Lojas.ToList();
        }
    }
}
