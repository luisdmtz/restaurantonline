using InduccionSemana4.Models.ContextConfiguration;
using Microsoft.EntityFrameworkCore;

namespace InduccionSemana4.Models.Contexto
{
    public class ContextoDatos : DbContext
    {
        public ContextoDatos(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            
            modelbuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelbuilder.ApplyConfiguration(new ClienteConfiguration());
            modelbuilder.ApplyConfiguration(new PedidoConfiguration());
            modelbuilder.ApplyConfiguration(new ProductoConfiguration());
            modelbuilder.ApplyConfiguration(new VendedoresConfiguration());


        }

        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
