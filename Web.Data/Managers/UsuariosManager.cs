using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Base;
using Web.Data.Entities;

namespace Web.Data.Managers
{
    public class UsuariosManager : BaseManager<Usuarios>
    {
        public async override Task<List<Usuarios>> BuscarListaAsync()
        {
            var respuesta = await contextoSingleton.Usuarios.Where(x=> x.Activo == true).ToListAsync();
            return respuesta;
        }

        public override Task<Usuarios> BuscarUno()
        {
            throw new NotImplementedException();
        }

        public async override Task<bool> Eliminar(Usuarios usuarios)
        {
            contextoSingleton.Entry<Usuarios>(usuarios).State = EntityState.Modified;

            var respuesta = await contextoSingleton.SaveChangesAsync() > 0;

            return respuesta;
        }
    }
}
