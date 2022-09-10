using Api.Interfaces;
using Microsoft.AspNetCore.Components;
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
                return null;
            }
        }

        public async Task<List<Usuarios>> GuardarAsync(Usuarios usuario)
        {
            try
            {
                var resultado = await _manager.Guardar(usuario, true);
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
