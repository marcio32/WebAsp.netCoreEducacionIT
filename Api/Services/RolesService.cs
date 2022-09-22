using Api.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Web.Data.Entities;
using Web.Data.Managers;

public class RolesService : IRolesService
{
    public string MensajeDeError { get; set; }
    public bool Estado { get; set; }

    private readonly RolesManager _manager;

    public RolesService()
    {
        _manager = new RolesManager();
    }

    public async Task<List<Roles>> BuscarListaAsync()
    {
        try
        {
            var resultado = await _manager.BuscarListaAsync();
            return resultado;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<Roles>> GuardarAsync(Roles rol)
    {
        try
        {
            var resultado = await _manager.Guardar(rol, rol.Id);
            return await _manager.BuscarListaAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<Roles>> EliminarAsync(Roles rol)
    {
        try
        {
            var resultado = await _manager.Eliminar(rol);
            return await _manager.BuscarListaAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}

