using InduccionSemana4.Models.Contexto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InduccionSemana4.Models.Consultas.ReadVendedor
{
    public class VendedorCrudSelectByZona
    {
        public class SelectZonaVendedor : IRequest<List<Vendedor>>
        {
            public string Zona { get; set; }
            public class GetProductByIdQueryHandler : IRequestHandler<SelectZonaVendedor, List<Vendedor>>
            {
                private readonly ContextoDatos _dataContext;
                public GetProductByIdQueryHandler(ContextoDatos context)
                {
                    _dataContext = context;
                }

                public async Task<List<Vendedor>> Handle(SelectZonaVendedor request, CancellationToken cancellationToken)
                {
                    var vendedor = await _dataContext.Vendedores.Where(x => x.Zona == request.Zona).ToListAsync();
                    if (vendedor == null) return null;
                    return vendedor;
                }
            }
        }
    }
}
