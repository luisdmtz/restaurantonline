using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InduccionSemana4.Models.ContextConfiguration
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasIndex(ven => ven.VendedorId).HasDatabaseName("UI_ProductoVendedor");

            builder.HasOne(typeof(Vendedor)).WithMany().OnDelete(DeleteBehavior.Restrict);

        }
    }
}
