using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandCliente
{
    public class ClienteCrudUpdate
    {
        public class InsertCliente : IRequest
        {
            public Cliente Cliente { get; set; }
        }

        public class HandlerInsertVendedor : IRequestHandler<InsertCliente>
        {
            private readonly ContextoDatos _contexto;

            public HandlerInsertVendedor(ContextoDatos contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(InsertCliente request, CancellationToken cancellationToken)
            {
                var cliente = new Cliente()
                {
                    Id = request.Cliente.Id,
                    NombreCliente = request.Cliente.NombreCliente,
                    Apellidos = request.Cliente.Apellidos,
                    ClaveCliente = request.Cliente.ClaveCliente,
                    Correo = request.Cliente.Correo,
                    Direccion = request.Cliente.Direccion,
                    Telefono = request.Cliente.Telefono,
                    Pwd = request.Cliente.Pwd
                };
                _contexto.Clientes.Update(cliente);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
