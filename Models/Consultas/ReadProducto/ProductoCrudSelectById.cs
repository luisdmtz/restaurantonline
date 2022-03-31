using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Consultas.ReadProducto
{
    public class ProductoCrudSelectById
    {
        public class SelectIdProducto : IRequest<Producto>
        {
            public int Id { get; set; }
            public class GetProductByIdQueryHandler : IRequestHandler<SelectIdProducto, Producto>
            {
                private readonly ContextoDatos _dataContext;
                public GetProductByIdQueryHandler(ContextoDatos context)
                {
                    _dataContext = context;
                }

                public async Task<Producto> Handle(SelectIdProducto request, CancellationToken cancellationToken)
                {
                    var producto = _dataContext.Productos.Where(x => x.Id == request.Id).FirstOrDefault();
                    if (producto == null) return null;
                    return producto;
                }
            }
        }
    }
}
