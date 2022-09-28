using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Web.Data.Entities;

namespace Web.ViewModels
{
    public class ProductosViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
        public bool Activo { get; set; }
        public IFormFile Imagen_Archivo { get; set; }

        public static implicit operator ProductosViewModel(Productos v)
        {
            var productoViewModel = new ProductosViewModel();
            productoViewModel.Id = v.Id;
            productoViewModel.Descripcion = v.Descripcion;
            productoViewModel.Precio = v.Precio;
            productoViewModel.Stock = v.Stock;
            productoViewModel.Imagen = v.Imagen;
            productoViewModel.Activo = v.Activo;
            return productoViewModel;
        }
    }
}
