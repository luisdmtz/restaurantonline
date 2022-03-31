using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandProducto
{
    public class ProductoCrudUpdate
    {
        public class UpdateProducto : IRequest
        {
            public Producto Producto { get; set; }
        }

        public class HandlerInsertVendedor : IRequestHandler<UpdateProducto>
        {
            private readonly ContextoDatos _contexto;

            public HandlerInsertVendedor(ContextoDatos contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(UpdateProducto request, CancellationToken cancellationToken)
            {
                var producto = new Producto()
                {
                    Descripcion = request.Producto.Descripcion,
                    ClaveProducto = request.Producto.ClaveProducto,
                    NombreProducto = request.Producto.NombreProducto,
                    Precio = request.Producto.Precio,
                    Stock = request.Producto.Stock,
                    VendedorId = request.Producto.VendedorId,
                    Categoria = request.Producto.Categoria
                };
                _contexto.Productos.Update(producto);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
