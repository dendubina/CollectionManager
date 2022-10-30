using System.Threading.Tasks;
using CollectionManager.WEB.Extensions;
using CollectionsManager.BLL.Extensions;
using CollectionsManager.BLL.Services.Interfaces;
using CollectionsManager.DAL.Constants;
using CollectionsManager.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CollectionManager.WEB.Middlewares
{
    public class LogoutMiddleware
    {
        private readonly RequestDelegate _next;

        public LogoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<User> userManager, IAuthService authService)
        {
            if (context.User.Identity is { IsAuthenticated: true })
            {
                var user = await userManager.FindByIdAsync(context.User.GetUserId());

                if (user is null || user.Status.Equals(UserStatus.Blocked))
                {
                    await LogoutAndRedirect(context, authService);
                }

                if (context.User.IsInRole(RoleNames.Admin.ToString()))
                {
                    if (!await userManager.IsInAdminRoleAsync(context.User.GetUserId()))
                    {
                        await LogoutAndRedirect(context, authService);
                    }
                }
            }

            await _next(context);
        }

        private static async Task LogoutAndRedirect(HttpContext context, IAuthService authService)
        {
            await authService.LogoutAsync();
            context.Response.Redirect("/Account/SignIn");
        }
    }
}
