using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InduccionSemana4.Models.ContextConfiguration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasIndex(cli => cli.ClienteId).HasDatabaseName("UI_PedidoCliente");

            builder.HasIndex(pro => pro.ProductoId).HasDatabaseName("UI_PedidoProducto");

            builder.HasOne(typeof(Cliente)).WithMany().OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(typeof(Producto)).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
