using Microsoft.AspNetCore.Mvc;
using Web.Data.Base;
using Web.Data.Entities;
using Web.ViewModels;

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
                var inicioViewModel = new InicioViewModel();
                inicioViewModel.Token = resultadoLogin.Value.ToString();
                return View("~/Views/Home/Index.cshtml", inicioViewModel);
            }
            else
            {
                return View("~/Views/Login/Login.cshtml");
            }
            
        }
    }
}
