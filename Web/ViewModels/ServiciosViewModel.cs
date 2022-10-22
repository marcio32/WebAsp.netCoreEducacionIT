using System.ComponentModel.DataAnnotations;
using Web.Data.Entities;

namespace Web.ViewModels
{
    public class ServiciosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator ServiciosViewModel(Servicios v)
        {
            var servicioViewModel = new ServiciosViewModel();
            servicioViewModel.Id = v.Id;
            servicioViewModel.Nombre = v.Nombre;
            servicioViewModel.Activo = v.Activo;
            return servicioViewModel;
        }

    }

}
