using Commons.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http;
using System.Runtime.InteropServices;
using Web.Data.Base;
using Web.Data.Entities;
using Web.Filters;
using Web.ViewModels;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly IHttpClientFactory _httpClient;

        public UsuariosController(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory;



        [Authorize]
        public IActionResult Usuarios()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UsuariosAddPartial([FromBody] Usuarios usuario)
        {
            var usuarioViewModel = new UsuariosViewModel();
            var baseApi = new BaseApi(_httpClient);
            var token = HttpContext.Session.GetString("Token");
            var roles = await baseApi.GetToApi("Roles/BuscarRoles", token);
            var resultadoRoles = roles as OkObjectResult;

            if (usuario != null)
            {
                usuario.Clave = EncryptHelper.Desencriptar(usuario.Clave);
                usuarioViewModel = usuario;
            }

            if (resultadoRoles != null)
            {
                var listaRoles = JsonConvert.DeserializeObject<List<Roles>>(resultadoRoles.Value.ToString());
                var listItemsRoles = new List<SelectListItem>();
                foreach (var item in listaRoles)
                {
                    listItemsRoles.Add(new SelectListItem { Text = item.Nombre, Value = item.Id.ToString() });
                }
                usuarioViewModel.Lista_Roles = listItemsRoles;
            }

            return PartialView("~/Views/Usuarios/Partial/usuariosAddPartial.cshtml", usuarioViewModel);
        }

        public async Task<IActionResult> EditarUsuario(Usuarios usuario)
        {
            var token = HttpContext.Session.GetString("Token");
            usuario.Clave = EncryptHelper.Encriptar(usuario.Clave);
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.PostToApi("Usuarios/GuardarUsuario", usuario, token);

            return await Task.Run(() => View("~/Views/Usuarios/usuarios.cshtml"));

        }

        public async Task<IActionResult> GuardarUsuario(Usuarios usuario)
        {
            var token = HttpContext.Session.GetString("Token");
            usuario.Clave = EncryptHelper.Encriptar(usuario.Clave);
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.PostToApi("Usuarios/GuardarUsuario", usuario, token);

            return await Task.Run(() => View("~/Views/Usuarios/usuarios.cshtml"));

        }

        public async Task<IActionResult> EliminarUsuario([FromBody] Usuarios usuario)
        {
            var token = HttpContext.Session.GetString("Token");
            usuario.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.PostToApi("Usuarios/EliminarUsuario", usuario, token);

            return await Task.Run(() => View("~/Views/Usuarios/usuarios.cshtml"));

        }
    }


}

