using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nextflow.Application.Utils;
using Nextflow.Domain.Enums;
using Nextflow.Domain.Interfaces.Repositories;

namespace Nextflow.Attributes;

public class RoleAuthorizeAttribute(params RoleEnum[] allowedRoles) : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user?.Identity?.IsAuthenticated != true)
        {
            context.Result = new UnauthorizedObjectResult("Acesso negado.");
            return;
        }
        var userId = TokenHelper.GetUserId(user);

        var userRepository = context.HttpContext.RequestServices.GetService<IUserRepository>();

        if (userRepository == null)
        {
            context.Result = new ObjectResult("Erro interno no servidor.") { StatusCode = 500 };
            return;
        }

        var authenticatedUser = userRepository.GetByIdAsync(userId, CancellationToken.None).Result;

        if (authenticatedUser == null)
        {
            context.Result = new UnauthorizedObjectResult("Usuário não encontrado.");
            return;
        }

        if (!allowedRoles.Contains(authenticatedUser.Role))
        {
            context.Result = new ForbidResult(); // Retorna 403 Forbidden
            return;
        }
    }
}