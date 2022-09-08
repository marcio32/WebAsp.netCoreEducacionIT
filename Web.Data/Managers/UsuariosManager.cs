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
            var respuesta = await contextoSingleton.Usuarios.ToListAsync();
            return respuesta;
        }

        public override Task<Usuarios> BuscarUno()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Delete()
        {
            throw new NotImplementedException();
        }
    }
}
