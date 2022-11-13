using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistWebVentaMVC.Models;
using SistWebVentaMVC.ModelsDA;
using SistWebVentaMVC.Utils;

namespace SistWebVentaMVC.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private UsuarioDA usuarioDa = new UsuarioDA();
        private RolDA rolDa = new RolDA();

        [Authorize(Roles = "ADMIN , EMPLEADO")]
        public IActionResult Index()
        {
            List<Usuario> lista = usuarioDa.ListarTodos();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            Usuario objUser = new Usuario();
            ViewBag.roles = Utileria.getRoles(rolDa.ListarTodos());
            ViewBag.estados = Utileria.getEstados();
            return View(objUser);
        }

        [HttpPost]
        public IActionResult Crear(Usuario obj)
        {
            if (usuarioDa.ExisteCorreo(0, obj.correo) == 0)
            {
                string msg = usuarioDa.Mantenimiento(obj,1);

                if (msg.Equals("OK"))
                {
                    TempData["mensaje_success"] = "El usuario se registro de forma correcta.";
                    return RedirectToAction("Index");
                }
                TempData["mensaje_error"] = msg.Equals("")? "Lo sentimos no se pudieron guardaron datos." : msg;
            }
            else
            {
                TempData["mensaje_error"] = "El correo " + obj.correo + " ya se encuentra registrado";
            }
            ViewBag.roles = Utileria.getRoles(rolDa.ListarTodos());
            ViewBag.estados = Utileria.getEstados();
            return View(obj);
        }

        public IActionResult Edit(int id = 0)
        {
            Usuario obj = usuarioDa.BuscarPorId(id);

            if (obj != null)
            {
                ViewBag.roles = Utileria.getRoles(rolDa.ListarTodos());
                ViewBag.generos = Utileria.getGeneros();
                ViewBag.estados = Utileria.getEstados();
                return View(obj);
            }

            TempData["mensaje_error"] = "No se encontraron datos del usuario con el id " + id + " .";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Usuario obj)
        {
            if (usuarioDa.ExisteCorreo(obj.idUser, obj.correo) == 0)
            {
                string msg = usuarioDa.Mantenimiento(obj,2);

                if (msg.Equals("OK"))
                {
                    TempData["mensaje_success"] = "El usuario se edito de forma correcta.";
                    return RedirectToAction("Index");
                }
                TempData["mensaje_error"] = msg.Equals("") ? "Lo sentimos no se pudieron editar datos." : msg;
            }
            else
            {
                TempData["mensaje_error"] = "El correo " + obj.correo + " ya se encuentra registrado";
            }
            ViewBag.roles = Utileria.getRoles(rolDa.ListarTodos());
            ViewBag.generos = Utileria.getGeneros();
            ViewBag.estados = Utileria.getEstados();
            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            Usuario obj = usuarioDa.BuscarPorId(id);

            if (obj != null)
            {
                string msg = usuarioDa.Mantenimiento(obj,3);
                if (msg.Equals("OK"))
                {
                    TempData["mensaje_success"] = "El usuario se elimino de forma correcta.";
                }
                else
                {
                    TempData["mensaje_error"] = msg.Equals("") ? "Lo sentimos no se pudieron eliminar datos." : msg;
                }
                return RedirectToAction("Index");
            }

            TempData["mensaje_error"] = "No se encontraron datos del usuario con el id " + id + " .";
            return RedirectToAction("Index");
        }
    }
}
