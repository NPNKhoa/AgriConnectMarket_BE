using AgriConnectMarket.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AgriConnectMarket.Infrastructure.JwtServices
{
    public class JwtTokenValidator(TokenValidationParameters _validationParameters) : ITokenValidator
    {
        private readonly JwtSecurityTokenHandler _handler = new JwtSecurityTokenHandler();

        public ClaimsPrincipal? ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            try
            {
                var principal = _handler.ValidateToken(token, _validationParameters, out SecurityToken validatedToken);

                // Optionally additional checks here (e.g., token alg, custom claim checks)
                return principal;
            }
            catch (SecurityTokenException)
            {
                // validation failed (signature, expiry, etc.)
                return null;
            }
            catch (Exception)
            {
                // unexpected error — consider logging and returning null
                return null;
            }
        }
    }
}
