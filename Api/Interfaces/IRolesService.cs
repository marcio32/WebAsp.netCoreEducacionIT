using Web.Data.Entities;

namespace Api.Interfaces
{
    public interface IRolesService
    {
        Task<List<Roles>> BuscarListaAsync();
        Task<List<Roles>> GuardarAsync(Roles roles);
        Task<List<Roles>> EliminarAsync(Roles roles);
    }
}
