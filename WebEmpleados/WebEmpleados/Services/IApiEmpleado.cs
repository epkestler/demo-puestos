using Microsoft.AspNetCore.Mvc.Rendering;
using WebEmpleados.Models;

namespace WebEmpleados.Services
{
    public interface IApiEmpleado
    {
        Task<List<EmpleadoViewModel>> EmpleadoListaAsync();
        Task<EmpleadoViewModel> EmpPorIdAsync(int id);
        Task EmpCrearAsync(EmpleadoViewModel model);
        Task EmpEditarAsync(int id, EmpleadoViewModel model);
        Task EmpEliminarAsync(int id);

        Task<List<PuestoViewModel>> PuestoListaAsync();
        Task<List<SelectListItem>> SelectPuestosAsync();
        Task<List<SelectListItem>> SelectJefesAsync(int? excluirId);

    }
}
