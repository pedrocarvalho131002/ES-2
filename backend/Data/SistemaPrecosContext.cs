using Microsoft.EntityFrameworkCore;
using SistemaPrecos.API.Models;

namespace SistemaPrecos.API.Data
{
    public class SistemaPrecosContext : DbContext
    {
        public SistemaPrecosContext(DbContextOptions<SistemaPrecosContext> options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<TipoUtilizador> TiposUtilizador { get; set; }
        public DbSet<TipoAcao> TiposAcao { get; set; }
        public DbSet<RegistoPreco> RegistoPrecos { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Se quiseres podes definir chaves compostas, relações ou constraints extra aqui.
        }
    }
}
