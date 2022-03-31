using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandPedido
{
    public class PedidoCrudDelete
    {
        public class DeletePedido : IRequest
        {
            public Pedido Pedido { get; set; }
        }

        public class HandlerDeleteVendedor : IRequestHandler<DeletePedido>
        {
            private readonly ContextoDatos _contexto;

            public HandlerDeleteVendedor(ContextoDatos contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(DeletePedido request, CancellationToken cancellationToken)
            {
                _contexto.Pedidos.Remove(request.Pedido);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
