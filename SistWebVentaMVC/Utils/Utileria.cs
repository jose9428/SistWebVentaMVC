using Microsoft.AspNetCore.Mvc.Rendering;
using SistWebVentaMVC.Models;

namespace SistWebVentaMVC.Utils
{
    public static class Utileria
    {
        public static List<SelectListItem> getGeneros()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem() { Text = "Masculino", Value = "M"});
            lista.Add(new SelectListItem() { Text = "Femenino", Value = "F" });
            return lista;
        }
        public static List<SelectListItem> getEstados()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            lista.Add(new SelectListItem() { Text = "Activo", Value = "1" });
            lista.Add(new SelectListItem() { Text = "Inactivo", Value = "0" });
            return lista;
        }
        public static SelectList getRoles(List<Rol> lista)
        {
            return new SelectList(lista, "id_rol", "nombre_rol");
        }


    }
}
