using Commons.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using Web.Data.Base;
using Web.Data.Entities;
using Web.ViewModels;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _smtpClient;

        public LoginController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory;
            _configuration = configuration;
            _smtpClient = new SmtpClient();
        }

        public IActionResult Login()
        {
            if (TempData["ErrorLogin"] != null)
                ViewBag.ErrorLogin = TempData["ErrorLogin"].ToString();
            return View();
        }

        public IActionResult OlvidoClave()
        {
            return View();
        }
        public IActionResult RecuperarCuenta()
        {
            return View();
        }


        public async Task<IActionResult> Ingresar(Login login)
        {
            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.PostToApi("Authenticate/Login", login, "");
            var resultadoLogin = token as OkObjectResult;

            if (resultadoLogin != null)
            {
                var resultadoSplit = resultadoLogin.Value.ToString().Split(";");
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimNombre = new Claim(ClaimTypes.Name, resultadoSplit[1]);
                Claim claimRole = new Claim(ClaimTypes.Role, resultadoSplit[2]);
                Claim claimEmail = new Claim(ClaimTypes.Email, resultadoSplit[3]);

                identity.AddClaim(claimNombre);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimEmail);

                ClaimsPrincipal usuarioPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, usuarioPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddDays(1)
                });


                HttpContext.Session.SetString("Token", resultadoSplit[0]);

                var inicioViewModel = new InicioViewModel();
                inicioViewModel.Token = resultadoSplit[0];
                return View("~/Views/Home/Index.cshtml", inicioViewModel);
            }
            else
            {
                TempData["ErrorLogin"] = "La contraseña o el mail no coinciden";
                return RedirectToAction("Login", "Login");

            }

        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        public async Task<IActionResult> EnviarMail(Login login)
        {
            var guid = Guid.NewGuid();
            var numeros = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(numeros.Substring(0, 6));
            var random = new Random(seed);
            var codigo = random.Next(000000, 999999);
            login.Codigo = codigo;

            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.PostToApi("RecuperarCuenta/GuardarCodigo", login, "");
            var resultadoLogin = response as OkObjectResult;

            if (resultadoLogin != null)
            {
                MailMessage mail = new();

                string CuerpoMail = CuerpoMailLogin(codigo);

                mail.From = new MailAddress(_configuration["ConfiguracionMail:Usuario"]);
                mail.To.Add(login.Mail);
                mail.Subject = "Codigo Recuperacion";
                mail.Body = CuerpoMail;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                _smtpClient.Host = _configuration["ConfiguracionMail:DireccionServidor"];
                _smtpClient.Port = int.Parse(_configuration["ConfiguracionMail:Puerto"]);
                _smtpClient.EnableSsl = true;
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential(_configuration["ConfiguracionMail:Usuario"], _configuration["ConfiguracionMail:Clave"]);

                _smtpClient.Send(mail);

                return RedirectToAction("RecuperarCuenta", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }

        }

        public async Task<IActionResult> CambiarClave(Login login)
        {
            login.Clave = EncryptHelper.Encriptar(login.Clave);
            var baseApi = new BaseApi(_httpClient);
            var response = await baseApi.PostToApi("RecuperarCuenta/CambiarClave", login, "");
            var resultadoLogin = response as OkObjectResult;

            if (resultadoLogin != null)
            {

                return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("Login", "Login");

            }

        }

        private static string CuerpoMailLogin(int codigo)
        {
            string separacion = "<br>";
            string mensaje = "<strong>A continuacion se mostrara un codigo que debera ingresar en la web de Educacion It</strong>";
            mensaje += $"{codigo} {separacion}";
            return mensaje;
        }

        public async Task LoginGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {

                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {

            var resultado = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = resultado.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Value,
                claim.Type,
                claim.Issuer,
                claim.OriginalIssuer
            });
            var login = new Login();
            login.Mail = claims.ToList()[4].Value;
            login.Google = true;


            var baseApi = new BaseApi(_httpClient);
            var token = await baseApi.PostToApi("Authenticate/Login", login, "");
            var resultadoLogin = token as OkObjectResult;

            if (resultadoLogin != null)
            {
                var resultadoSplit = resultadoLogin.Value.ToString().Split(";");

                HttpContext.Session.SetString("Token", resultadoSplit[0]);

                var inicioViewModel = new InicioViewModel();
                inicioViewModel.Token = resultadoSplit[0];
                return View("~/Views/Home/Index.cshtml", inicioViewModel);
            }
            else
            {
                TempData["ErrorLogin"] = "La contraseña o el mail no coinciden";
                return RedirectToAction("Login", "Login");

            }

        }

    }
}
