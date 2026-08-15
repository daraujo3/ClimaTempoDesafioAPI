using System.Security.Claims;

namespace ClimaTempoDesafioAPI.Helpers.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUsuarioId(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                throw new UnauthorizedAccessException();

            return int.Parse(claim.Value);
        }
    }
}
