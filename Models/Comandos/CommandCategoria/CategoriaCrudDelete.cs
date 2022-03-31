using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandCategoria
{
    public class CategoriaCrudDelete
    {
        public class InsertCliente : IRequest
        {
            public Categoria Categoria { get; set; }
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
                
                _contexto.Categorias.Remove(request.Categoria);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
