using AnimalShelter.Web.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AnimalShelter.Web.CustomFilters
{
    public class AdminOrAssistant : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(context.HttpContext.User.Identity.Name))
            {
                context.Result = new RedirectToActionResult("Index", "NotAuthenticatedUser", null);
                return;
            }

            var isNotAdminOrAssistant = !UserIsAdminOrAssistant(context);

            if (isNotAdminOrAssistant)
            {
                context.Result = new RedirectToActionResult("Index", "Unauthorized", null);
                return;
            }

        }

        private bool UserIsAdminOrAssistant(AuthorizationFilterContext context)
        {
            var _roleApplicationService = context.HttpContext.RequestServices.GetRequiredService<RoleApplicationService>();

            var roleResult = _roleApplicationService.IsUserAnAdminOrAssistant(context.HttpContext.User.Identity.Name);

            if (roleResult.IsFailure)
            {
                return false;
            }

            return roleResult.Value;
        }
    }
}
