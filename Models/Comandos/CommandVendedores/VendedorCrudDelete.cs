using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandVendedores
{
    public class VendedorCrudDelete
    {
        public class DeleteVendedor : IRequest
        {
            public Vendedor vendedor { get; set; }
        }

        public class HandlerDeleteVendedor : IRequestHandler<DeleteVendedor>
        {
            private readonly ContextoDatos _contexto;

            public HandlerDeleteVendedor(ContextoDatos contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(DeleteVendedor request, CancellationToken cancellationToken)
            {
                _contexto.Vendedores.Remove(request.vendedor);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
