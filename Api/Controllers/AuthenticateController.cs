using Commons.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Data;
using Web.Data.Entities;

namespace Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
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
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            try
            {
                login.Clave = EncryptHelper.Encriptar(login.Clave);
                var usuario = contextInstance.Usuarios.Where(x => x.Mail == login.Mail && x.Clave == login.Clave).Include(x => x.Roles).FirstOrDefault();
                if(usuario != null)
                {
                    var Claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, usuario.Mail),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    Claims.Add(new Claim(ClaimTypes.Role, usuario.Roles.Nombre));

                    var token = CrearToken(Claims);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token).ToString() + ";" + usuario.Nombre + ";" + usuario.Roles.Nombre + ";" + usuario.Mail);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch(Exception ex)
            {
                await GenerateLogHelper.LogError(ex, "AuthenticateController", "Login");
                return null;
            }
        }

        private JwtSecurityToken CrearToken(List<Claim> autenticar)
        {
            try
            {
                var firma = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secreta"]));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(24),
                    claims: autenticar,
                    signingCredentials: new SigningCredentials(firma, SecurityAlgorithms.HmacSha256)
                    );
                return token;
            }
            catch (Exception ex)
            {
                GenerateLogHelper.LogError(ex, "AuthenticateController", "CrearToken");
                return null;
            }
            
        }


    }
}
