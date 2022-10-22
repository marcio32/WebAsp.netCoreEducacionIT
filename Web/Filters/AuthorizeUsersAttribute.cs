using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters
{
    public class AuthorizeUsersAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var usuarios = context.HttpContext.User;
            if (usuarios.Identity.IsAuthenticated == false)
            {
                RouteValueDictionary rutalogin = new RouteValueDictionary(
                    new
                    {
                        controller = "Login",
                        action = "Login"
                    });
                RedirectToRouteResult resultado = new RedirectToRouteResult(rutalogin);
                context.Result = resultado;
            }
        }
    }
}
