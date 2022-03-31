using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InduccionSemana4.Models
{
    public class Vendedor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClaveVendedor { get; set; }
        public string NombreNegocio { get; set; }
        public string TitularNegocio { get; set; }
        public string Direccion { get; set; }
        public string Zona { get; set; }
        public string Telefono { get; set; }
        public string FotoNegocio { get; set; }
        public string Correo { get; set; }
        public string Pwd { get; set; }

    }
}
