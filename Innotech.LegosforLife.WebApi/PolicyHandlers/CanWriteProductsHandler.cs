using System.Threading.Tasks;
using InnoTech.LegosForLife.Security;
using InnoTech.LegosForLife.Security.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace InnoTech.LegosForLife.WebApi.PolicyHandlers
{
    public class CanWriteProductsHandler: AuthorizationHandler<CanWriteProductsHandler>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanWriteProductsHandler handler)
        {
            var defaultContext = context.Resource as DefaultHttpContext;
            if (defaultContext != null)
            {
                var user = defaultContext.Items["LoginUser"] as LoginUser;
                if (user != null)
                {
                    var authService = defaultContext.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                    var permissions = authService.GetPermissions(user.Id);
                    if (permissions.Exists(p => p.Name.Equals("CanWriteProducts")))
                    {
                        context.Succeed(handler);
                    }
                    else
                    {
                        context.Fail();
                    }
                    
                }
                else
                {
                    // not logged in
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}