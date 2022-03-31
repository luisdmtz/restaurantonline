using InduccionSemana4.Models.Contexto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InduccionSemana4.Models.Consultas.ReadPedido
{
    public class PedidoCrudSelect
    {
        public class SelectProducto : IRequest<List<Pedido>> { }

        public class Handler : IRequestHandler<SelectProducto, List<Pedido>>
        {
            private readonly ContextoDatos _dataContext;
            public Handler(ContextoDatos dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<List<Pedido>> Handle(SelectProducto request, CancellationToken cancellationToken)
            {
                return await _dataContext.Pedidos.ToListAsync();
            }
        }
    }
}
