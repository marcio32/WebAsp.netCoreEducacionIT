using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Data.Entities;

namespace Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {

        private readonly IConfiguration _configuration;
        private static ApplicationDbContext contextInstance;
        public AuthenticateController(IConfiguration configuration)
        {
            contextInstance = new ApplicationDbContext();
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            try
            {
                var usuario = contextInstance.Usuarios.Where(x => x.Mail == login.Mail && x.Clave == login.Clave).Include(x => x.Roles).FirstOrDefault();
                if(usuario != null)
                {
                    return Ok(usuario.Mail);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
