using System.ComponentModel.DataAnnotations;

namespace ApiEmpleados.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Codigo { get; set; }

        [Required(AllowEmptyStrings = false), StringLength(100)]
        public string Nombre { get; set; }

        [Required, Range(1,int.MaxValue)]
        public int IdPuesto { get; set; }

        public int? IdJefe { get; set; }

        public string NombrePuesto { get; set; } = string.Empty;
    }
}
