using System.Security.Claims;

namespace APIRutaU.Helpers.Token
{
    public interface ITokenHelper
    {
        string CreateToken(IEnumerable<Claim>? claims, TimeSpan expiration);
    }
}
