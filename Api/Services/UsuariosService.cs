using Api.Interfaces;
using Commons.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Logging;
using Web.Data.Entities;
using Web.Data.Managers;

namespace Api.Services
{
    public class UsuariosService : IUsuariosService
    {
        public string MensajeDeError { get; set; }
        public bool Estado { get; set; }

        private readonly UsuariosManager _manager;

        public UsuariosService()
        {
            _manager = new UsuariosManager();
        }

        public async Task<List<Usuarios>> BuscarListaAsync()
        {
            try
            {
                var resultado = await _manager.BuscarListaAsync();
                return resultado;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Usuarios>> GuardarAsync(Usuarios usuario)
        {
            try
            {
                var resultado = await _manager.Guardar(usuario, usuario.Id);
                return await _manager.BuscarListaAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Usuarios>> EliminarAsync(Usuarios usuario)
        {
            try
            {
                var resultado = await _manager.Eliminar(usuario);
                return await _manager.BuscarListaAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
