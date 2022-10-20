using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Base;
using Web.Data.Entities;

namespace Web.Data.Managers
{
    public class ServiciosManager : BaseManager<Servicios>
    {
        public async override Task<List<Servicios>> BuscarListaAsync()
        {
            var respuesta = contextoSingleton.Servicios.FromSqlRaw($"ObtenerServicios").ToList();
            return respuesta;
        }

        public override Task<Servicios> BuscarUno()
        {
            throw new NotImplementedException();
        }

        public async override Task<bool> Eliminar(Servicios servicio)
        {

            throw new NotImplementedException();
        }

        public async Task<List<Servicios>> GuardarAsync(Servicios servicio)
        {
            var p = contextoSingleton.Database.ExecuteSqlRaw($"GuardaroActualizarServicios {servicio.Id}, {servicio.Nombre}, {servicio.Activo}");
            return contextoSingleton.Servicios.FromSqlRaw($"ObtenerServicios").ToList();
        }

        public async Task<List<Servicios>> EliminarAsync(Servicios servicio)
        {
            var p = contextoSingleton.Database.ExecuteSqlRaw($"EliminarServicio {servicio.Id}");
            return contextoSingleton.Servicios.FromSqlRaw($"ObtenerServicios").ToList();
        }
    }
}