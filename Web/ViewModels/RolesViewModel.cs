using System.ComponentModel.DataAnnotations;
using Web.Data.Entities;

namespace Web.ViewModels
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator RolesViewModel(Roles v)
        {
            var rolViewModel = new RolesViewModel();
            rolViewModel.Id = v.Id;
            rolViewModel.Nombre = v.Nombre;
            rolViewModel.Activo = v.Activo;
            return rolViewModel;
        }

    }

}
