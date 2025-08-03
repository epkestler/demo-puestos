using ApiEmpleados.Models;
using ApiEmpleados.Services;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ApiEmpleados.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IBaseService<Empleado> _empleadoServ;

        public EmpleadoController(IBaseService<Empleado> emp)
        {
            _empleadoServ = emp;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var res = await _empleadoServ.PorIdAsync(id);
            return res!= null? Ok(res): NotFound();
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<Empleado>>> Lista() {
            return Ok( await _empleadoServ.ListaAsync());
        }
        /*[HttpGet("arbol")]
        public async Task<ActionResult<List<EmpleadoNodo>>> GetArbol()
        {
            var resultado = await _db.QueryAsync("sp_ObtenerArbolJerarquia", commandType: CommandType.StoredProcedure);

            // Convertir a árbol en C# si SP devuelve lista plana
            var empleados = resultado.Select(row => new EmpleadoNodo
            {
                Id = row.Id,
                Codigo = row.Codigo,
                Nombre = row.Nombre,
                Puesto = row.Puesto,
            }).ToList();

            foreach (var nodo in empleados)
            {
                nodo.Subordinados = empleados.Where(x => row.Codigo_Jefe == nodo.Id).ToList();
            }

            var raiz = empleados.Where(e => resultado.First(r => r.Id == e.Id).Codigo_Jefe == null).ToList();

            return Ok(raiz);
        }*/

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Empleado emp)
        {
            await _empleadoServ.CrearAsync(emp);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] Empleado emp)
        {
            var myemp = await _empleadoServ.PorIdAsync(id);
            if (myemp == null)
                return NotFound("Registro no encontrado");

            await _empleadoServ.EditarAsync(id, emp);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var myemp = await _empleadoServ.PorIdAsync(id);
            if (myemp == null)
                return NotFound("Registro no encontrado");

            await _empleadoServ.EliminarAsync(id);
            return Ok();
        }
    }

}
