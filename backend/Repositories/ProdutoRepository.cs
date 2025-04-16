using SistemaPrecos.API.Data;
using SistemaPrecos.API.Models;

namespace SistemaPrecos.API.Repositories
{
    public class ProdutoRepository
    {
        private readonly SistemaPrecosContext _context;

        public ProdutoRepository(SistemaPrecosContext context)
        {
            _context = context;
        }

        // Métodos específicos serão implementados conforme necessário
    }
}
