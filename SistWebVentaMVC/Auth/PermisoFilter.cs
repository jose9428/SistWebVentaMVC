using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SistWebVentaMVC.Models;

namespace SistWebVentaMVC.Auth
{
    public class PermisoFilter : ActionFilterAttribute
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public Usuario ObtenerUsuario(ActionExecutingContext context)
        {
            return JsonConvert.DeserializeObject<Usuario>((context.HttpContext.Session.GetString("usuario")));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            JsonResult jsonResult;
      
            //Obtenemos los datos del usuario
            Usuario logeado = ObtenerUsuario(context);
       

            //Obtenemos los datos del controller
            var controller = (Controller) context.Controller;

            if (logeado.id_rol != 1)
            {
                jsonResult = new JsonResult(new { message = "Error Messages" })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                context.Result = jsonResult;
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
