using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Web.Data.Base;
using Web.Data.Entities;
using Web.Filters;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public ServiciosController(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory;


        [Authorize]
        public IActionResult Servicios()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ServiciosAddPartial([FromBody] Servicios servicio)
        {
            var serviciosViewModel = new ServiciosViewModel();
            if(servicio != null)
            {
                serviciosViewModel = servicio;
            }
        
            return PartialView("~/Views/Servicios/Partial/serviciosAddPartial.cshtml", serviciosViewModel);
        }

        public async Task<IActionResult> EditarServicio(Servicios servicio)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var servicios = await baseApi.PostToApi("Servicios/GuardarServicio", servicio, token);

            return await Task.Run(() => View("~/Views/Servicios/servicios.cshtml"));

        }

        public async Task<IActionResult> GuardarServicio(Servicios servicio)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            var servicios = await baseApi.PostToApi("Servicios/GuardarServicio", servicio, token);

            return await Task.Run(() => View("~/Views/Servicios/servicios.cshtml"));

        }

        public async Task<IActionResult> EliminarServicio([FromBody] Servicios servicios)
        {
            var token = HttpContext.Session.GetString("Token");
            servicios.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.PostToApi("Servicios/EliminarServicio", servicios,token);

            return await Task.Run(() => View("~/Views/Servicios/servicios.cshtml"));

        }
    }
}
