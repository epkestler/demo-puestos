using ApiEmpleados.Models;

namespace ApiEmpleados.Services
{
    public interface IBaseService<T>
    {
        Task<T> PorIdAsync(int Id);
        Task CrearAsync(T dto);
        Task EditarAsync(int id, T dto);
        Task EliminarAsync(int id);
        Task<List<T>> ListaAsync();
    }
}
