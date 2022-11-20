using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistWebVentaMVC.Auth;
using SistWebVentaMVC.Models;
using SistWebVentaMVC.ModelsDA;
using SistWebVentaMVC.Utils;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace SistWebVentaMVC.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        private ProductoDA prodDA = new ProductoDA();

       // [PermisoFilter]
        public IActionResult Index()
        {
            List<Producto> lista = prodDA.ListarTodos();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            Producto obj = new Producto();
            ViewBag.estados = Utileria.getEstados();
            return View(obj);
        }

        [HttpPost]
        public IActionResult Crear(Producto obj, IFormFile? archivo)
        {
            if (archivo != null)
            {
                obj.imagen = Archivo.GuardarArchivo(archivo , prodDA.MaxId());
            }

            string msg = prodDA.Mantenimiento(obj, 1);

            if (msg.Equals("OK"))
            {
                TempData["mensaje_success"] = "El producto se registro de forma correcta.";
                return RedirectToAction("Index");
            }

            TempData["mensaje_error"] = msg.Equals("") ? "Lo sentimos no se pudieron guardaron datos." : msg;

            ViewBag.estados = Utileria.getEstados();
            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int id = 0)
        {
            Producto obj = prodDA.BuscarPorId(id);

            if (obj != null)
            {
                ViewBag.estados = Utileria.getEstados();
                return View(obj);
            }

            TempData["mensaje_error"] = "No se encontraron datos del producto con el id " + id + " .";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Producto obj, IFormFile? archivo)
        {
            if (archivo != null)
            {
                obj.imagen = Archivo.GuardarArchivo(archivo, obj.id_prod);
            }

            string msg = prodDA.Mantenimiento(obj, 2);

            if (msg.Equals("OK"))
            {
                TempData["mensaje_success"] = "El producto se actualizo de forma correcta.";
                return RedirectToAction("Index");
            }

            TempData["mensaje_error"] = msg.Equals("") ? "Lo sentimos no se pudieron actualizar datos." : msg;

            ViewBag.estados = Utileria.getEstados();
            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            Producto obj = prodDA.BuscarPorId(id);

            if (obj != null)
            {
                string msg = prodDA.Mantenimiento(obj, 3);
                if (msg.Equals("OK"))
                {
                    Archivo.EliminarArchivo(obj.imagen);
                    TempData["mensaje_success"] = "El producto "+obj.nombre+" se elimino de forma correcta.";
                }
                else
                {
                    TempData["mensaje_error"] = msg.Equals("") ? "Lo sentimos no se pudieron eliminar datos." : msg;
                }
                return RedirectToAction("Index");
            }

            TempData["mensaje_error"] = "No se encontraron datos del producto con el id " + id + " .";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public FileResult ExportarExcel()
        {

            DataTable dt = prodDA.ListarTodosDt();
            dt.TableName = "Productos";

            using (XLWorkbook libro = new XLWorkbook())
            {
                var hoja = libro.Worksheets.Add(dt);

                hoja.ColumnsUsed().AdjustToContents();
                MemoryStream stream = new MemoryStream();
                libro.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Reporte " + DateTime.Now.ToString() + ".xlsx");

            }
        }

    }
}
