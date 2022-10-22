using Web.Data.Entities;

namespace Api.Interfaces
{
    public interface IServiciosService
    {
        Task<List<Servicios>> BuscarListaAsync();
        Task<List<Servicios>> GuardarAsync(Servicios servicios);
        Task<List<Servicios>> EliminarAsync(Servicios servicios);
    }
}
