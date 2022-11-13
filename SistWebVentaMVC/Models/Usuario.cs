using SistWebVentaMVC.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistWebVentaMVC.Models
{
    public class Usuario: Rol
    {
        public int idUser { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(20, ErrorMessage = "Longitud maxima de 20 caracteres")]
        [RegularExpression(Validacion.LETRAS, ErrorMessage = "Solo se permiten letras")]
        public string nombres { get; set; }


        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El apellido es requerido")]
        [RegularExpression(Validacion.LETRAS, ErrorMessage = "Solo se permiten letras")]
        [MaxLength(30)]
        public string apellidos { get; set; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El correo es requerido")]
        [RegularExpression(Validacion.CORREO, ErrorMessage = "El correo no cumple con un formato válido")]
        [MaxLength(50)]
        public string correo { get; set; }

        [Display(Name = "Genero")]
        public string genero { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "La fecha de nacimiento es requerido")]
        public DateTime fechaNac { get; set; }

        [Display(Name = "Sueldo")]
        [Required(ErrorMessage = "El sueldo es requerido")]

        [RegularExpression(Validacion.DECIMAL, ErrorMessage ="Solo se permiten numeros")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal sueldo { get; set; }
        public string valor { get; set; }

        [Display(Name = "Estado")]
        public int estado { get; set; }
    }
}
