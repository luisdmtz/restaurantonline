using InduccionSemana4.Models.Contexto;
using MediatR;

namespace InduccionSemana4.Models.Comandos.CommandCategoria
{
    public class CategoriaCrudInsert
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
                var categoria = new Categoria()
                {
                    
                    ClaveCategoria = request.Categoria.ClaveCategoria,
                    Descripcion = request.Categoria.Descripcion
                };
                _contexto.Categorias.Add(categoria);
                if (await _contexto.SaveChangesAsync() > 0) return Unit.Value;
                throw new Exception("Problemas guardando los cambios");
            }
        }
    }
}
