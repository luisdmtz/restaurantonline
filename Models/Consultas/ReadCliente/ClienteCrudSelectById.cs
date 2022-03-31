using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Consultas.ReadCliente
{
    public class ClienteCrudSelectById
    {
        public class SelectIdCliente : IRequest<Cliente>
        {
            public int Id { get; set; }
            public class GetProductByIdQueryHandler : IRequestHandler<SelectIdCliente, Cliente>
            {
                private readonly ContextoDatos _dataContext;
                public GetProductByIdQueryHandler(ContextoDatos context)
                {
                    _dataContext = context;
                }

                public async Task<Cliente> Handle(SelectIdCliente request, CancellationToken cancellationToken)
                {
                    var cliente = _dataContext.Clientes.Where(x => x.Id == request.Id).FirstOrDefault();
                    if (cliente == null) return null;
                    return cliente;
                }
            }
        }
    }
}
