using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandProducto
{
    public class ProductoCrudDelete
    {
        public class DeleteProducto : IRequest
        {
            public Producto producto { get; set; }
        }

        public class HandlerDeleteVendedor : IRequestHandler<DeleteProducto>
        {
            private readonly ContextoDatos _contexto;

            public HandlerDeleteVendedor(ContextoDatos contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(DeleteProducto request, CancellationToken cancellationToken)
            {
                _contexto.Productos.Remove(request.producto);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
