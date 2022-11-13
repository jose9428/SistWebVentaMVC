using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistWebVentaMVC.Models;
using SistWebVentaMVC.ModelsDA;
using System.Security.Claims;

namespace SistWebVentaMVC.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            Usuario obj = new Usuario();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario obj)
        {
            UsuarioDA usuarioDA = new UsuarioDA();
            Usuario logeado = usuarioDA.Autentificar(obj);

            if (logeado != null)
            {
                if (logeado.estado == 1)
                {
                    #region AUTENTICACTION
                    List<Claim> claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, logeado.nombres + " "+logeado.apellidos),
                        new Claim("Correo", logeado.correo),
                        new Claim("Codigo", logeado.idUser.ToString()),
                        new Claim(ClaimTypes.Role,logeado.nombre_rol)
                      //  new Claim(ClaimTypes.Role,"EMPLEADO") // SI hubiera mas role se agrega
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                    #endregion
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["mensaje_error"] = "Lo sentimos, su cuenta se encuentra inactiva.";
                }
            }
            else
            {
                TempData["mensaje_error"] = "Correo y/o contraseña incorrecto.";
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["mensaje_success"] = "La sesión se ha cerrado de forma correcta.";
            return RedirectToAction("Login");
        }
        public void GuardarSesion(Usuario usuario)
        {
            HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuario));
        }

        public void BorrarSesion()
        {
            HttpContext.Session.Remove("usuario");
        }

        public Usuario ObtenerSesion()
        {
            return JsonConvert.DeserializeObject<Usuario>((HttpContext.Session.GetString("usuario")));
        }
    }
}
