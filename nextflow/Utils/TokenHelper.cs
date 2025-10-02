using System.Security.Claims;
using nextflow.Domain.Exceptions;

namespace nextflow.Application.Utils;

public static class TokenHelper
{
    public static Guid GetUserId(ClaimsPrincipal user)
    {
        if (user?.Identity?.IsAuthenticated != true)
        {
            throw new NotAuthorizedException("Usuário não autenticado.");
        }

        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new NotAuthorizedException("O ID do usuário no token é inválido ou não está presente.");
        }

        return userId;
    }
}