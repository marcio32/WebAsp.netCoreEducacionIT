using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Entities;


[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class ProductosController : Controller
{
    [HttpGet]
    [Route("BuscarProductos")]
    public async Task<List<Productos>> BuscarProductos()
    {
        var buscarProductos = new ProductosService();
        var p = await buscarProductos.BuscarListaAsync();
        return p;
    }

    [HttpPost]
    [Route("GuardarProducto")]
    public async Task<List<Productos>> GuardarProducto(Productos productos)
    {
        var buscarProducto = new ProductosService();
        return await buscarProducto.GuardarAsync(productos);
    }

    [HttpPost]
    [Route("EliminarProducto")]
    public async Task<List<Productos>> EliminarProducto(Productos productos)
    {
        var buscarProducto = new ProductosService();
        return await buscarProducto.EliminarAsync(productos);
    }
}

