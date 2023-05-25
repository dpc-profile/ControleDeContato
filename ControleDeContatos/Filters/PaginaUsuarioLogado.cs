using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

using ControleDeContatos.Models;

namespace ControleDeContatos.Filters
{
    // Filtro que valida se o usuário está logado
    public class PaginaUsuarioLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Pega a key sessaoUsuarioLogado do usuário atual
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            // Se for nula, redireciona para a página de Login
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = redirecionaParaLogin();
            }
            else
            {
                // Se não for nula, deserializa o objeto da sessão do usuário
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                // Se for nula, redireciona para a página de Login
                if (usuario == null)
                {
                    context.Result = redirecionaParaLogin();
                }
            }

            // Se tudo deu certo, devolve o context
            base.OnActionExecuting(context);
        }

        public RedirectToRouteResult redirecionaParaLogin()
        {
            return new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
        }
    }
}