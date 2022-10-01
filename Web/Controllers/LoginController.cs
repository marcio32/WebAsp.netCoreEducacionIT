using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimNombre = new Claim(ClaimTypes.Name, "Marcio");
                Claim claimRole = new Claim(ClaimTypes.Role, "Administrador");
                Claim claimEmail = new Claim("EmailUsuario", login.Mail);

                identity.AddClaim(claimNombre);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimEmail);

                ClaimsPrincipal usuarioPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, usuarioPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddDays(1)
                });


                var inicioViewModel = new InicioViewModel();
                inicioViewModel.Token = resultadoLogin.Value.ToString();
                return View("~/Views/Home/Index.cshtml", inicioViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }

        }
    
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
