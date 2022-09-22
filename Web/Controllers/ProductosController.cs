using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.InteropServices;
using Web.Data.Base;
using Web.Data.Entities;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductosController : Controller
    {

        private readonly IHttpClientFactory _httpClient;

        public ProductosController(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory;


        public IActionResult Productos()
        {
            return View();
        }

        public async Task<IActionResult> ProductosAddPartial([FromBody] Usuarios producto)
        {
            var productoViewModel = new ProductosViewModel();

            return PartialView("~/Views/Productos/Partial/productosAddPartial.cshtml", productoViewModel);
        }

    }


}
