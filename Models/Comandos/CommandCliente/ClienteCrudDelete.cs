using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandCliente
{
    public class ClienteCrudDelete
    {
        public class DeleteProducto : IRequest
        {
            public Cliente Cliente { get; set; }
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
                _contexto.Clientes.Remove(request.Cliente);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
