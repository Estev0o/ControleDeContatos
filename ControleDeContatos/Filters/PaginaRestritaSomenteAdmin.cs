using ControleDeContatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleDeContatos.Filters
{
    public class PaginaRestritaSomenteAdmin : ActionFilterAttribute
    {
        public object UsuarioModel { get; private set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "action", "Index" } });
            }

            else
            {
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                if (usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "action", "Index" } });
                }

                if (usuario.Perfil != Enum.PerfilEnum.admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Restrito" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
