using InduccionSemana4.Models.Contexto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InduccionSemana4.Models.Consultas.ReadProducto
{
    public class ProductoCrudSelect
    {
        public class SelectProducto : IRequest<List<Producto>> { }

        public class Handler : IRequestHandler<SelectProducto, List<Producto>>
        {
            private readonly ContextoDatos _dataContext;
            public Handler(ContextoDatos dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<List<Producto>> Handle(SelectProducto request, CancellationToken cancellationToken)
            {
                return await _dataContext.Productos.ToListAsync();
            }
        }
    }
}
