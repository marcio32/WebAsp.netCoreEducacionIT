using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Entities;


[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class RolesController : Controller
{
    [HttpGet]
    [Route("BuscarRoles")]
    public async Task<List<Roles>> BuscarRoles()
    {
        var buscarRoles = new RolesService();
        var p = await buscarRoles.BuscarListaAsync();
        return p;
    }

    [HttpPost]
    [Route("GuardarRol")]
    public async Task<List<Roles>> GuardarRol(Roles roles)
    {
        var buscarRol = new RolesService();
        return await buscarRol.GuardarAsync(roles);
    }

    [HttpPost]
    [Route("EliminarRol")]
    public async Task<List<Roles>> EliminarRol(Roles roles)
    {
        var buscarRol = new RolesService();
        return await buscarRol.EliminarAsync(roles);
    }
}

