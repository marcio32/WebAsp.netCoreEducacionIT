using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Web.Data.Base;
using Web.Data.Entities;
using Web.Filters;
using Web.ViewModels;

namespace Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public RolesController(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory;


        [AuthorizeUsers (Policy = "ADMINISTRADORES")]
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
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.PostToApi("Roles/GuardarRol", rol, token);

            return await Task.Run(() => View("~/Views/Roles/roles.cshtml"));

        }

        public async Task<IActionResult> GuardarRol(Roles rol)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var roles = await baseApi.PostToApi("Roles/GuardarRol", rol, token);

            return await Task.Run(() => View("~/Views/Roles/roles.cshtml"));

        }

        public async Task<IActionResult> EliminarRol([FromBody] Roles roles)
        {
            var token = HttpContext.Session.GetString("Token");
            roles.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.PostToApi("Roles/EliminarRol", roles,token);

            return await Task.Run(() => View("~/Views/Roles/roles.cshtml"));

        }
    }
}
