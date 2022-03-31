using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Consultas.ReadVendedor
{
    public class VendedorCrudSelectById
    {
        public class SelectIdVendedor : IRequest<Vendedor>
        {
            public int Id { get; set; }
            public class GetProductByIdQueryHandler : IRequestHandler<SelectIdVendedor, Vendedor>
            {
                private readonly ContextoDatos _dataContext;
                public GetProductByIdQueryHandler(ContextoDatos context)
                {
                    _dataContext = context;
                }

                public async Task<Vendedor> Handle(SelectIdVendedor request, CancellationToken cancellationToken)
                {
                    var vendedor = _dataContext.Vendedores.Where(x => x.Id == request.Id).FirstOrDefault();
                    if (vendedor == null) return null;
                    return vendedor;
                }
            }
        }
    }
}
