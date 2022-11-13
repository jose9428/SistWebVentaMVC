using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistWebVentaMVC.Models;
using SistWebVentaMVC.ModelsDA;

namespace SistWebVentaMVC.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class RolController : Controller
    {
        private RolDA rolDA = new RolDA();
        public IActionResult Index()
        {
            List<Rol> lista = rolDA.ListarTodos();
            return View(lista);
        }
    }
}
