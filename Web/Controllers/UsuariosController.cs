using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Web.Data.Base;
using Web.Data.Entities;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {


        public IActionResult Usuarios()
        {
            return View();
        }

        public async Task<IActionResult> GuardarUsuario(Usuarios usuario)
        {
            //var baseApi = new BaseApi(_httpClient);
            //var usuarios = await baseApi.PostToApi("Usuarios/GuardarUsuario", usuario);

            return null;

        }
    }

    
}
