using System.ComponentModel.DataAnnotations;
using Web.Data.Entities;

namespace Web.ViewModels
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

    }
}
