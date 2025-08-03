using Microsoft.AspNetCore.Mvc.Rendering;
using WebEmpleados.Models;

namespace WebEmpleados.Services
{
    // Services/EmpleadoApiClient.cs
    public class EmpleadoApiClient : IApiEmpleado
    {
        private readonly HttpClient _http;

        public EmpleadoApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EmpleadoViewModel>> EmpleadoListaAsync()
        {
            var response = await _http.GetFromJsonAsync<List<EmpleadoViewModel>>("api/empleado/lista");
            return response ?? new();
        }

        public async Task<EmpleadoViewModel> EmpPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<EmpleadoViewModel>($"api/empleado/{id}");
        }

        public async Task EmpCrearAsync(EmpleadoViewModel model)
        {
            await _http.PostAsJsonAsync("api/empleado", model);
        }

        public async Task EmpEditarAsync(int id, EmpleadoViewModel model)
        {
            await _http.PutAsJsonAsync($"api/empleado/{id}", model);
        }

        public async Task EmpEliminarAsync(int id)
        {
            await _http.DeleteAsync($"api/empleado/{id}");
        }


        public async Task<List<PuestoViewModel>> PuestoListaAsync()
        {
            var response = await _http.GetFromJsonAsync<List<PuestoViewModel>>("api/puesto/lista");
            return response ?? new();
        }

        public async Task<List<SelectListItem>> SelectPuestosAsync()
        {
            var puestos = await _http.GetFromJsonAsync<List<PuestoViewModel>>("api/puesto/lista");
            return puestos.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nombre
            }).ToList();
        }

        public async Task<List<SelectListItem>> SelectJefesAsync(int? excluirId = null)
        {
            var empleados = await EmpleadoListaAsync();

            if (excluirId.HasValue)
                empleados = empleados.Where(e => e.Id != excluirId.Value).ToList();

            return empleados.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.Nombre} ({e.NombrePuesto})"
            }).ToList();
        }
    }

}
