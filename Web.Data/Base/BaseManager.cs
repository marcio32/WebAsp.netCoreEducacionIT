using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Base
{
    public abstract class BaseManager<T> where T : class
    {

        #region Singleton Contexto
        protected static ApplicationDbContext _context;


        public static ApplicationDbContext contextoSingleton
        {
            get
            {
                if (_context == null)
                    _context = new ApplicationDbContext();
                return _context;
            }
        }

        #endregion


        #region  Metodos Abstractos
        public abstract Task<List<T>> BuscarListaAsync();
        public abstract Task<T> BuscarUno();
        public abstract Task<bool> Eliminar(T modelo);
        #endregion


        #region Metodos Publicos
        public async Task<bool> Guardar(T modelo, int id)
        {
            if (id == 0)
                contextoSingleton.Entry<T>(modelo).State = EntityState.Added;

            else
                contextoSingleton.Entry<T>(modelo).State = EntityState.Modified;

            var result = await contextoSingleton.SaveChangesAsync() > 0;
            contextoSingleton.Entry<T>(modelo).State = EntityState.Detached;

            return result;
        }
        #endregion
    }
}
