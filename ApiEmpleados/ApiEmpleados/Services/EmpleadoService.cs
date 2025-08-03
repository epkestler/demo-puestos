using ApiEmpleados.Models;
using ApiEmpleados.Util;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace ApiEmpleados.Services
{
    public class EmpleadoService : IBaseService<Empleado>
    {
        private readonly IDbConnection _db;
        private readonly IBaseService<Puesto> _puestoRepo;

        public EmpleadoService(IDbConnection repo, IBaseService<Puesto> puestoRepo)
        {
            _db = repo;
            _puestoRepo = puestoRepo;
        }

        public async Task CrearAsync(Empleado emp)
        {
            var esValida = await ValidarJerarquiaAsync(emp);
            if (!esValida)
                throw new BusinessLogicException("La jerarquía de puestos no es válida.");

            var param = new DynamicParameters();
            param.Add("@iCodigo", emp.Codigo);
            param.Add("@iNombre", emp.Nombre);
            param.Add("@iIdPuesto", emp.IdPuesto);
            param.Add("@iIdJefe", emp.IdJefe);

            await _db.ExecuteAsync("procEmpleadoInsert", param, commandType: CommandType.StoredProcedure);
        }

        public async Task<Empleado> PorIdAsync(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@iId", id);
            return (await _db.QueryAsync<Empleado>("procEmpleadoId", parametros, commandType: CommandType.StoredProcedure)).FirstOrDefault();
        }

        public async Task<List<Empleado>> ListaAsync()
        {
            var listado = (await _db.QueryAsync<Empleado>("procEmpleadoBusqueda", commandType: CommandType.StoredProcedure)).ToList();
            
            return listado;
        }

        public async Task EditarAsync(int id, Empleado dto)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@iId", id);
            parametros.Add("@iCodigo", dto.Codigo);
            parametros.Add("@iNombre", dto.Nombre);
            parametros.Add("@iIdPuesto", dto.IdPuesto);
            parametros.Add("@iIdJefe", dto.IdJefe);

            await _db.ExecuteAsync("procEmpleadoUpdate", parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task EliminarAsync(int id)
        {
            await _db.ExecuteAsync("procEmpleadoDelete", new { iId = id }, commandType: CommandType.StoredProcedure);
        }

        private Task<bool> ValidarJerarquiaAsync(Empleado dto) {
            return Task.FromResult(true);
        }
    }

}
