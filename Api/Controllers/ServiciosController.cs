using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Entities;


[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ServiciosController : Controller
{
    [HttpGet]
    [Route("BuscarServicios")]
    public async Task<List<Servicios>> BuscarServicios()
    {
        var buscarServicios = new ServiciosService();
        var p = await buscarServicios.BuscarListaAsync();
        return p;
    }

    [HttpPost]
    [Route("GuardarServicio")]
    public async Task<List<Servicios>> GuardarServicio(Servicios servicios)
    {
        var buscarServicio = new ServiciosService();
        return await buscarServicio.GuardarAsync(servicios);
    }

    [HttpPost]
    [Route("EliminarServicio")]
    public async Task<List<Servicios>> EliminarServicio(Servicios servicios)
    {
        var buscarServicio = new ServiciosService();
        return await buscarServicio.EliminarAsync(servicios);
    }
}

