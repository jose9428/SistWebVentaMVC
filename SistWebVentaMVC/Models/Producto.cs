using System.ComponentModel.DataAnnotations;

namespace SistWebVentaMVC.Models
{
    public class Producto
    {
        public int id_prod { get; set; }

		[Display(Name = "Nombre")]
		[Required(ErrorMessage = "El nombre es requerido")]
		[MaxLength(70, ErrorMessage = "Longitud maxima de {0} caracteres")]
		public string nombre { get; set; }

		[Required(ErrorMessage = "El precio es requerido")]
		public decimal precio { get; set; }

		[Required(ErrorMessage = "El stock es requerido")]
		public int stock { get; set; }
		public int estado { get; set; }
		public string imagen { get; set; }

	}
}
