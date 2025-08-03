using ApiEmpleados.Models;
using ApiEmpleados.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ApiEmpleados.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuestoController : ControllerBase
    {
        private readonly IBaseService<Puesto> _puestoService;

        public PuestoController(IBaseService<Puesto> srv)
        {
            _puestoService = srv;
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<Puesto>>> GetPuestos()
        {
            return Ok(await _puestoService.ListaAsync());
        }
        //TODO: el crud de puestos ;)
    }
}
