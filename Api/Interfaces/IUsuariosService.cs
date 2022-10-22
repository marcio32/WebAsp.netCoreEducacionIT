using Web.Data.Entities;

namespace Api.Interfaces
{
    public interface IUsuariosService
    {
        Task<List<Usuarios>> BuscarListaAsync();
        Task<List<Usuarios>> GuardarAsync(Usuarios usuarios);
        Task<List<Usuarios>> EliminarAsync(Usuarios usuarios);
    }
}
