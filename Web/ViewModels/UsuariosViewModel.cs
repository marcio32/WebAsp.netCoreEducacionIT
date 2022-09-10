﻿using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class UsuariosViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo es obligatorio")]
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public string Mail { get; set; }
        public int Id_Rol { get; set; }
        public bool Activo { get; set; }
        public string Clave { get; set; }
        public int? Codigo { get; set; }
    }
}
