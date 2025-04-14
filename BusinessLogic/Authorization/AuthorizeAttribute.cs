using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Domain.Entities;
using BackendApi.Authorization;
using Microsoft.AspNetCore.Http;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Linq;

namespace BusinessLogic.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Roles> _roles;

        public AuthorizeAttribute(params Roles[] roles)
        {
            _roles = roles ?? new Roles[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Проверяем, есть ли атрибут [AllowAnonymous]
            var allowAnonymous = context.Filters
                .OfType<AllowAnonymousFilter>()
                .Any();

            if (allowAnonymous)
                return;

            // Остальная логика авторизации
            var account = context.HttpContext.Items["User"] as User;
            if (account == null ||
                (_roles.Any() && (!account.Roles.HasValue || !_roles.Contains(account.Roles.Value))))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}