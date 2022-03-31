using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Consultas.ReadPedido
{
    public class PedidoCrudSelectGetById
    {
        public class SelectIdPedido : IRequest<Pedido>
        {
            public int Id { get; set; }
            public class GetProductByIdQueryHandler : IRequestHandler<SelectIdPedido, Pedido>
            {
                private readonly ContextoDatos _dataContext;
                public GetProductByIdQueryHandler(ContextoDatos context)
                {
                    _dataContext = context;
                }

                public async Task<Pedido> Handle(SelectIdPedido request, CancellationToken cancellationToken)
                {
                    var pedido = _dataContext.Pedidos.Where(x => x.Id == request.Id).FirstOrDefault();
                    if (pedido == null) return null;
                    return pedido;
                }
            }
        }
    }
}
