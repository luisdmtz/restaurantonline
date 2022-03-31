using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InduccionSemana4.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClaveProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public int VendedorId { get; set; }

    }
}
