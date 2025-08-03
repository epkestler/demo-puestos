using ApiEmpleados.Models;

namespace ApiEmpleados.Services
{
    public interface IPuestoService
    {
        Task<List<Puesto>> ListaAsync();
        Task<Puesto> PorIdAsync(int id);
    }
}
