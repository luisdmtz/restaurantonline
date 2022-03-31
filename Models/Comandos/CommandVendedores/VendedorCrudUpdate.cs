using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandVendedores
{
    public class VendedorCrudUpdate
    {
        public class UpdateVendedor : IRequest
        {
            public Vendedor Vendedor { get; set; }
        }

        public class HandlerInsertVendedor : IRequestHandler<UpdateVendedor>
        {
            private readonly ContextoDatos _contexto;

            public HandlerInsertVendedor(ContextoDatos contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(UpdateVendedor request, CancellationToken cancellationToken)
            {
                var vendedor = new Vendedor()
                {
                    Id = request.Vendedor.Id,
                    ClaveVendedor = request.Vendedor.ClaveVendedor,
                    Direccion = request.Vendedor.Direccion,
                    NombreNegocio = request.Vendedor.NombreNegocio,
                    FotoNegocio = request.Vendedor.FotoNegocio,
                    Telefono = request.Vendedor.Telefono,
                    TitularNegocio = request.Vendedor.TitularNegocio,
                    Zona = request.Vendedor.Zona,
                    Correo = request.Vendedor.Correo,
                    Pwd = request.Vendedor.Pwd
                };
                _contexto.Vendedores.Update(vendedor);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
