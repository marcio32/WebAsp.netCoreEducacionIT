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
    public class ProductosManager : BaseManager<Productos>
    {
        public async override Task<List<Productos>> BuscarListaAsync()
        {
            var respuesta = await contextoSingleton.Productos.Where(x => x.Activo == true).ToListAsync();
            return respuesta;
        }

        public override Task<Productos> BuscarUno()
        {
            throw new NotImplementedException();
        }

        public async override Task<bool> Eliminar(Productos producto)
        {
            contextoSingleton.Entry<Productos>(producto).State = EntityState.Modified;

            var respuesta = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry<Productos>(producto).State = EntityState.Detached;

            return respuesta;
        }
    }
}
