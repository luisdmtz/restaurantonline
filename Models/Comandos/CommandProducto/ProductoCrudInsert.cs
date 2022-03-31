using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandProducto
{
    public class ProductoCrudInsert
    {
        public class InsertProducto : IRequest
        {
            public Producto Producto { get; set; }
        }

        public class HandlerInsertVendedor : IRequestHandler<InsertProducto>
        {
            private readonly ContextoDatos _contexto;

            public HandlerInsertVendedor(ContextoDatos contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(InsertProducto request, CancellationToken cancellationToken)
            {
                var producto = new Producto()
                {
                    Descripcion = request.Producto.Descripcion,
                    ClaveProducto = request.Producto.ClaveProducto,
                    NombreProducto = request.Producto.NombreProducto,
                    Precio = request.Producto.Precio,
                    Stock = request.Producto.Stock,
                    Categoria = request.Producto.Categoria,
                    VendedorId = request.Producto.VendedorId
                };
                _contexto.Productos.Add(producto);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
