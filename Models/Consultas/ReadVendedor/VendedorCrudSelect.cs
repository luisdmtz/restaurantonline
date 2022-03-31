using InduccionSemana4.Models.Contexto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InduccionSemana4.Models.Consultas.ReadVendedor
{
    public class VendedorCrudSelect
    {
        public class SelectVendedor : IRequest<List<Vendedor>> { }

        public class Handler : IRequestHandler<SelectVendedor, List<Vendedor>>
        {
            private readonly ContextoDatos _dataContext;
            public Handler(ContextoDatos dataContext)
            {
                _dataContext = dataContext;
            }
            public async Task<List<Vendedor>> Handle(SelectVendedor request, CancellationToken cancellationToken)
            {
                return await _dataContext.Vendedores.ToListAsync();
            }
        }

    }
}
