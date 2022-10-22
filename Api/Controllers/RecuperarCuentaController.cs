using Api.Services;
using Commons.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Entities;


[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class RecuperarCuentaController : Controller
{
    private readonly IConfiguration _configuration;
    private static ApplicationDbContext contextInstance;
    public RecuperarCuentaController(IConfiguration configuration)
    {
        contextInstance = new ApplicationDbContext();
        _configuration = configuration;
    }

    [HttpPost]
    [Route("GuardarCodigo")]
    public bool GuardarCodigo([FromBody] Login login)
    {
        try
        {
            var GuardarCodigo = new RecuperarCuentaService();
            var usuario = contextInstance.Usuarios.Where(x => x.Mail == login.Mail).FirstOrDefault();
            {
                usuario.Codigo = login.Codigo;
                return GuardarCodigo.GuardarCodigo(usuario);
            }
            return false;
        } 
        catch (Exception ex)
        {
        }
       
    }

    [HttpPost]
    [Route("CambiarClave")]
    public bool CambiarClave([FromBody] Login login)
    {
        try
        {
            var GuardarCodigo = new RecuperarCuentaService();
            var usuario = contextInstance.Usuarios.Where(x => x.Mail == login.Mail && x.Codigo == login.Codigo).FirstOrDefault();
            if (usuario != null)
            {
                usuario.Codigo = null;
                usuario.Clave = login.Clave;
                return GuardarCodigo.GuardarCodigo(usuario);
            }
            return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

}

