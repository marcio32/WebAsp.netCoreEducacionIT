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
    public class RolesManager : BaseManager<Roles>
    {
        public async override Task<List<Roles>> BuscarListaAsync()
        {
            var respuesta = await contextoSingleton.Roles.Where(x => x.Activo == true).ToListAsync();
            return respuesta;
        }

        public override Task<Roles> BuscarUno()
        {
            throw new NotImplementedException();
        }

        public async override Task<bool> Eliminar(Roles rol)
        {
            contextoSingleton.Entry<Roles>(rol).State = EntityState.Modified;

            var respuesta = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry<Roles>(rol).State = EntityState.Detached;

            return respuesta;
        }
    }
}
