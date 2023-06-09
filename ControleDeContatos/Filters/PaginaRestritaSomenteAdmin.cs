using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

using ControleDeContatos.Models;

namespace ControleDeContatos.Filters
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PaginaRestritaSomenteAdminAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });

            }
            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
                
                if (usuario.Perfil != Enums.PerfilEnums.Admin)
                {
                    context.Result = new RedirectToRouteResult(
                            new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }


            }

            base.OnActionExecuting(context);
        }
    }
}