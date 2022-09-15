using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Runtime.InteropServices;
using Web.Data.Base;
using Web.Data.Entities;
using Web.ViewModels;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly IHttpClientFactory _httpClient;

        public UsuariosController(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory;


        public IActionResult Usuarios()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UsuariosAddPartial([FromBody] Usuarios usuario)
        {
            var usuarioViewModel = new UsuariosViewModel();
            if(usuario != null)
            {
                usuarioViewModel = usuario;
            }
            return PartialView("~/Views/Usuarios/Partial/usuariosAddPartial.cshtml", usuarioViewModel);
        }

        public async Task<IActionResult> GuardarUsuario(Usuarios usuario)
        {
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.PostToApi("Usuarios/GuardarUsuario", usuario);

            return await Task.Run(() => View("~/Views/Usuarios/usuarios.cshtml"));

        }

        public async Task<IActionResult> EliminarUsuario([FromBody] Usuarios usuario)
        {
            usuario.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.PostToApi("Usuarios/EliminarUsuario", usuario);

            return await Task.Run(() => View("~/Views/Usuarios/usuarios.cshtml"));

        }
    }

    
}
