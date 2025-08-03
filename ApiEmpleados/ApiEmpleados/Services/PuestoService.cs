using ApiEmpleados.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiEmpleados.Services
{
    public class PuestoService : IBaseService<Puesto>
    {
        
        private readonly IDbConnection _db;

        public PuestoService(IDbConnection conn)
        {
            _db = conn;
        }

        public async Task<Puesto> PorIdAsync(int Id)
        {
            throw new NotImplementedException();
        }
        public async Task CrearAsync(Puesto dto)
        {
            throw new NotImplementedException();
        }
        public async Task EditarAsync(int id, Puesto dto)
        {
            throw new NotImplementedException();
        }
        public async Task EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Puesto>> ListaAsync()
        {
            return (List<Puesto>)await _db.QueryAsync<Puesto>("procPuestosLista", commandType: CommandType.StoredProcedure);
        }
    }


}
