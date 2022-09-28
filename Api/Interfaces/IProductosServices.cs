using Web.Data.Entities;

namespace Api.Interfaces
{
    public interface IProductosService
    {
        Task<List<Productos>> BuscarListaAsync();
        Task<List<Productos>> GuardarAsync(Productos productos);
        Task<List<Productos>> EliminarAsync(Productos productos);
    }
}
