using System.Security.Claims;

namespace EngExam.Extensions
{
    public static class ClaimsExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)//claim nay chua thong tin user id trong request
        {

            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("nameid")?.Value; ;
            return Guid.TryParse(value, out var guid) ? guid : Guid.Empty;
        }
    }
}
