using Microsoft.AspNetCore.Mvc;
using Web.Data.Base;
using Web.Data.Entities;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public LoginController(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory;
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Ingresar(Login login)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.PostToApi("Authenticate/Login", login);
            var resultadoLogin = token as OkObjectResult;

            if(resultadoLogin != null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                return View("~/Views/Login/Login.cshtml");
            }
            
        }
    }
}
