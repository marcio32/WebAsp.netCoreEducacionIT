using Api.Interfaces;
using Commons.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Web.Data.Entities;
using Web.Data.Managers;

public class ServiciosService : IServiciosService
{
    public string MensajeDeError { get; set; }
    public bool Estado { get; set; }

    private readonly ServiciosManager _manager;

    public ServiciosService()
    {
        _manager = new ServiciosManager();
    }

    public async Task<List<Servicios>> BuscarListaAsync()
    {
        try
        {
            var resultado = await _manager.BuscarListaAsync();
            return resultado;
        }
        catch (Exception ex)
        {
            await GenerateLogHelper.LogError(ex, "ServiciosService", "BuscarListaASync");
            return null;
        }
    }

    public async Task<List<Servicios>> GuardarAsync(Servicios rol)
    {
        try
        {
            var resultado = await _manager.GuardarAsync(rol);
            return resultado;
        }
        catch (Exception ex)
        {
            await GenerateLogHelper.LogError(ex, "ServiciosService", "GuardarAsync");
            return null;
        }
    }

    public async Task<List<Servicios>> EliminarAsync(Servicios rol)
    {
        try
        {
            var resultado = await _manager.EliminarAsync(rol);
            return resultado;
        }
        catch (Exception ex)
        {
            await GenerateLogHelper.LogError(ex, "ServiciosService", "EliminarAsync");
            return null;
        }
    }
}

