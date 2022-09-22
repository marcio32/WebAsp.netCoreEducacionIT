using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Web.Data.Base;
using Web.Data.Entities;
using Web.ViewModels;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public RolesController(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory;

        public IActionResult Roles()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RolesAddPartial([FromBody] Roles rol)
        {
            var rolesViewModel = new RolesViewModel();
            if(rol != null)
            {
                rolesViewModel = rol;
            }
        
            return PartialView("~/Views/Roles/Partial/rolesAddPartial.cshtml", rolesViewModel);
        }

        public async Task<IActionResult> EditarRol(Roles rol)
        {
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.PostToApi("Roles/GuardarRol", rol);

            return await Task.Run(() => View("~/Views/Roles/roles.cshtml"));

        }

        public async Task<IActionResult> GuardarRol(Roles rol)
        {
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.PostToApi("Roles/GuardarRol", rol);

            return await Task.Run(() => View("~/Views/Roles/roles.cshtml"));

        }

        public async Task<IActionResult> EliminarRol([FromBody] Roles roles)
        {
            roles.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.PostToApi("Roles/EliminarRol", roles);

            return await Task.Run(() => View("~/Views/Roles/roles.cshtml"));

        }
    }
}
