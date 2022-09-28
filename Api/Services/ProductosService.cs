using Api.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Web.Data.Entities;
using Web.Data.Managers;

public class ProductosService : IProductosService
{
    public string MensajeDeError { get; set; }
    public bool Estado { get; set; }

    private readonly ProductosManager _manager;

    public ProductosService()
    {
        _manager = new ProductosManager();
    }

    public async Task<List<Productos>> BuscarListaAsync()
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

    public async Task<List<Productos>> GuardarAsync(Productos producto)
    {
        try
        {
            var resultado = await _manager.Guardar(producto, producto.Id);
            return await _manager.BuscarListaAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<Productos>> EliminarAsync(Productos producto)
    {
        try
        {
            var resultado = await _manager.Eliminar(producto);
            return await _manager.BuscarListaAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}

