using Api.Interfaces;
using Commons.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Web.Data.Entities;
using Web.Data.Managers;

public class RecuperarCuentaService
{
    public string MensajeDeError { get; set; }
    public bool Estado { get; set; }

    private readonly RecuperarCuentaManager _manager;

    public RecuperarCuentaService()
    {
        _manager = new RecuperarCuentaManager();
    }

    public bool GuardarCodigo(Usuarios usuario)
    {
        try
        {
            var resultado =  _manager.Guardar(usuario, usuario.Id);
            return resultado.Result;
        }
        catch (Exception ex)
        {
            GenerateLogHelper.LogError(ex, "RecuperarCuentaService", "GuardarCodigo");
            return false;
        }
    }
}

