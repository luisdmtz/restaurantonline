using InduccionSemana4.Models.Contexto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InduccionSemana4.Models.Consultas.ReadCategoria
{
    public class CategoriaCrudSelect
    {
        public class SelectCategoria : IRequest<List<Cliente>> { }

        public class Handler : IRequestHandler<SelectCategoria, List<Cliente>>
        {
            private readonly ContextoDatos _dataContext;
            public Handler(ContextoDatos dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<List<Cliente>> Handle(SelectCategoria request, CancellationToken cancellationToken)
            {
                return await _dataContext.Clientes.ToListAsync();
            }
        }
    }
}
