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
    public class RecuperarCuentaManager : BaseManager<Usuarios>
    {
        public override Task<List<Usuarios>> BuscarListaAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Usuarios> BuscarUno()
        {
            throw new NotImplementedException();
        }

        public override Task<bool> Eliminar(Usuarios modelo)
        {
            throw new NotImplementedException();
        }
    }
}
