using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Runtime.InteropServices;
using Web.Data.Base;
using Web.Data.Entities;
using Web.Filters;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProductosController : Controller
    {

        private readonly IHttpClientFactory _httpClient;

        public ProductosController(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory;


        [Authorize]
        public IActionResult Productos()
        {
            return View();
        }

        public async Task<IActionResult> ProductosAddPartial([FromBody] Productos producto)
        {
            var productoViewModel = new ProductosViewModel();

            if(producto != null)
            {
                productoViewModel = producto;
            }

            return PartialView("~/Views/Productos/Partial/productosAddPartial.cshtml", productoViewModel);
        }

        public async Task<IActionResult> EditarProducto(Productos producto)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = HttpContext.Session.GetString("Token");
            if (producto.Imagen_Archivo != null && producto.Imagen_Archivo.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    producto.Imagen_Archivo.CopyTo(ms);
                    var imagenBytes = ms.ToArray();
                    producto.Imagen = Convert.ToBase64String(imagenBytes);
                }
            }
            producto.Imagen_Archivo = null;
            var productos = await baseApi.PostToApi("Productos/GuardarProducto", producto, token);

            return await Task.Run(() => View("~/Views/Productos/productos.cshtml"));

        }

        public async Task<IActionResult> GuardarProducto(Productos producto)
        {
            var token = HttpContext.Session.GetString("Token");
            var baseApi = new BaseApi(_httpClient);
            if(producto.Imagen_Archivo != null && producto.Imagen_Archivo.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    producto.Imagen_Archivo.CopyTo(ms);
                    var imagenBytes = ms.ToArray();
                    producto.Imagen = Convert.ToBase64String(imagenBytes);
                }
            }
            producto.Imagen_Archivo = null;
            var productos = await baseApi.PostToApi("Productos/GuardarProducto", producto, token);

            return await Task.Run(() => View("~/Views/Productos/productos.cshtml"));

        }

        public async Task<IActionResult> EliminarProducto([FromBody] Productos productos)
        {
            var token = HttpContext.Session.GetString("Token");
            productos.Activo = false;
            var baseApi = new BaseApi(_httpClient);
            var usuarios = await baseApi.PostToApi("Productos/EliminarProducto", productos,token);

            return await Task.Run(() => View("~/Views/Productos/productos.cshtml"));

        }

    }


}
