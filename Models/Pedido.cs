using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InduccionSemana4.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClavePedido { get; set; }
        public double PrecioTotal { get; set; }
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Requerido")]
        public int ProductoId { get; set; }
    }
}
