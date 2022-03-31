using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandPedido
{
    public class PedidoCrudUpdate
    {
        public class UpdatePedido : IRequest
        {
            public Pedido Pedido { get; set; }
        }

        public class HandlerInsertVendedor : IRequestHandler<UpdatePedido>
        {
            private readonly ContextoDatos _contexto;

            public HandlerInsertVendedor(ContextoDatos contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(UpdatePedido request, CancellationToken cancellationToken)
            {
                var pedido = new Pedido()
                {
                    Cantidad = request.Pedido.Cantidad,
                    ClavePedido = request.Pedido.ClavePedido,
                    PrecioTotal = request.Pedido.PrecioTotal,
                    ClienteId = request.Pedido.ClienteId,
                    ProductoId = request.Pedido.ProductoId
                };
                _contexto.Pedidos.Update(pedido);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
