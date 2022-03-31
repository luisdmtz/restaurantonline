using InduccionSemana4.Models.Contexto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InduccionSemana4.Models.Consultas.ReadCliente
{
    public class ClienteCrudSelect
    {
        public class SelectCliente : IRequest<List<Cliente>> { }

        public class Handler : IRequestHandler<SelectCliente, List<Cliente>>
        {
            private readonly ContextoDatos _dataContext;
            public Handler(ContextoDatos dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<List<Cliente>> Handle(SelectCliente request, CancellationToken cancellationToken)
            {
                return await _dataContext.Clientes.ToListAsync();
            }
        }
    }
}
