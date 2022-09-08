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
        { get 
            { 
                if(_context == null)
                    _context = new ApplicationDbContext();
                return _context;
            } 
        }

        #endregion


        #region  Metodos Abstractos
        public abstract Task<List<T>> BuscarListaAsync();
        public abstract Task<T> BuscarUno();
        public abstract Task<bool> Delete();
        #endregion

    }
}
