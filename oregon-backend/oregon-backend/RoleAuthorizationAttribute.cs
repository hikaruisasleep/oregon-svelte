using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace oregon_backend;

public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public RoleAuthorizeAttribute(int role)
    {
        _role = role.ToString();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var role = context.HttpContext.Items["Role"]?.ToString();
        if (role != _role)
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
        }
    }
}