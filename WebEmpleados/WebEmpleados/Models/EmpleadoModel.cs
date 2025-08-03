using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebEmpleados.Models
{
    public class EmpleadoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el código de empleado"), StringLength(20, ErrorMessage = "máximo 20 caracteres")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        public int? IdJefe { get; set; }


        [Required(ErrorMessage = "Seleccione un puesto"), Range(1, int.MaxValue)]
        public int IdPuesto { get; set; }
        public string NombrePuesto { get; set; } = string.Empty;
        public List<EmpleadoViewModel> Subordinados { get; set; } = new List<EmpleadoViewModel>() { };

        //combos
        public IEnumerable<SelectListItem>? PuestosDisponibles { get; set; }
        public IEnumerable<SelectListItem>? JefesDisponibles { get; set; }

    }
}
