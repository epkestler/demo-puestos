using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebEmpleados.Models;
using WebEmpleados.Services;

namespace WebEmpleados.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IApiEmpleado _cliente;

        public EmpleadoController(IApiEmpleado cliente)
        {
            _cliente = cliente;
        }

        public async Task<IActionResult> Index()
        {
            var empleados = await _cliente.EmpleadoListaAsync();
            return View(empleados);
        }

        public async Task<IActionResult> Jerarquia()
        {
            var empleados = await _cliente.EmpleadoListaAsync();
            return View(empleados);
        }


        #region nuevo empleado
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var emp = new EmpleadoViewModel { 
                PuestosDisponibles = await _cliente.SelectPuestosAsync(),
                JefesDisponibles = await _cliente.SelectJefesAsync(null)
            };
            return View(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PuestosDisponibles = await _cliente.SelectPuestosAsync();
                model.JefesDisponibles = await _cliente.SelectJefesAsync(null);

                return View(model);
            }

            await _cliente.EmpCrearAsync(model);
            return RedirectToAction(nameof(Jerarquia));
        }
        #endregion

        #region editar empleado
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var empleado = await _cliente.EmpPorIdAsync(id);
            if (empleado == null) return NotFound();

            var model = new EmpleadoViewModel
            {
                Id = empleado.Id,
                Codigo = empleado.Codigo,
                Nombre = empleado.Nombre,
                IdJefe = empleado.IdJefe,
                IdPuesto = empleado.IdPuesto,
                PuestosDisponibles = await _cliente.SelectPuestosAsync(),
                JefesDisponibles = await _cliente.SelectJefesAsync(empleado.Id)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PuestosDisponibles = await _cliente.SelectPuestosAsync();
                model.JefesDisponibles = await _cliente.SelectJefesAsync(id);

                return View(model);
            }

            await _cliente.EmpEditarAsync(id, model);
            return RedirectToAction("Empleado/Jerarquia");
        }

        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existe = await _cliente.EmpPorIdAsync(id);
            if (existe == null)
                return NotFound();

            await _cliente.EmpEliminarAsync(id);
            return RedirectToAction(nameof(Jerarquia));
        }
    }

}
