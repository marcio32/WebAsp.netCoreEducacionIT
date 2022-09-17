using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Entities;

namespace Api.Controllers
{

    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {

        [HttpGet]
        [Route("BuscarUsuarios")]
        public async Task<List<Usuarios>> BuscarUsuarios()
        {
            var buscarUsuarios = new UsuariosService();
            var p = await buscarUsuarios.BuscarListaAsync();
            return p;
        }

        [HttpPost]
        [Route("GuardarUsuario")]
        public async Task<List<Usuarios>> GuardarUsuario(Usuarios usuarios)
        {
            var buscarUsuarios = new UsuariosService();
            return await buscarUsuarios.GuardarAsync(usuarios);
        }

        [HttpPost]
        [Route("EliminarUsuario")]
        public async Task<List<Usuarios>> EliminarUsuario(Usuarios usuarios)
        {
            var buscarUsuarios = new UsuariosService();
            return await buscarUsuarios.EliminarAsync(usuarios);
        }
    }

}
