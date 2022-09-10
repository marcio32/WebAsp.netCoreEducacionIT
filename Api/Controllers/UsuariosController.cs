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

        [HttpGet("BuscarUsuarios", Name = "BuscarUsuarios")]
        public async Task<List<Usuarios>> BuscarUsuarios()
        {
            var buscarUsuarios = new UsuariosService();
            return await buscarUsuarios.BuscarListaAsync();
        }

        [HttpPost("GuardarUsuario", Name = "GuardarUsuario")]
        public async Task<List<Usuarios>> GuardarUsuario(Usuarios usuarios)
        {
            var buscarUsuarios = new UsuariosService();
            return await buscarUsuarios.GuardarAsync(usuarios);
        }
    }

}
