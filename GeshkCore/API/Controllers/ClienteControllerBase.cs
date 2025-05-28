using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeshkCore.API.Controllers
{
    [ApiController]
    public abstract class ClienteControllerBase : ControllerBase
    {
        protected Guid ObtenerClientId()
        {
            var claim = User.FindFirst("client_id")?.Value;
            if (claim == null)
                throw new UnauthorizedAccessException("Token sin client_id");

            return Guid.Parse(claim);
        }

        protected Guid ObtenerUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
        }

        protected string ObtenerRol()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value ?? "cliente";
        }
    }
}
