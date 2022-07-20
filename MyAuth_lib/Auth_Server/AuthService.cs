using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using MyAuth_lib.Auth_Server.Models;
using MyAuth_lib.Exceptions;
using MyAuth_lib.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static MyAuth_lib.Constants.AuthConstants;

namespace MyAuth_lib.Auth_Server
{
    public class AuthService : IAuthService
    {
        protected readonly IIdentityRepository identityRepository;
        protected readonly IAuthServerSupplier supplier;

        public AuthService(IIdentityRepository identityRepository, IAuthServerSupplier supplier)
        {
            this.identityRepository = identityRepository;
            this.supplier = supplier;
        }

        public AuthResult Authenticate(AuthRequest authRequest)
        {
            var user = identityRepository.TryGetUser(authRequest);

            if (user == null)
            {
                throw new LoginFailedException($"Login failed, no user with specified data! Data: {authRequest}");
            }

            var expiration = CreateExpiration();
            var token = CreateToken(user, expiration);

            return CreateAuthResult(user, token, expiration);
        }

        protected virtual string CreateToken(User user, DateTime expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(ENCRYPTION_KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateClaimsIdentity(user),
                Expires = expiration,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = AUDIENCE,
                Issuer = ISSUER
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        protected virtual ClaimsIdentity CreateClaimsIdentity(User user)
        {
            return new ClaimsIdentity(CreateClaims(user));
        }

        protected virtual IEnumerable<Claim> CreateClaims(User user)
        {
            yield return new Claim(JwtRegisteredClaimNames.Sub, TOKEN_SUBJECT);
            yield return new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            yield return new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString());
            yield return new Claim(ClaimTypes.Name, "TODO");
            
            //TODO permissions
        }

        protected virtual DateTime CreateExpiration()
        {
            return DateTime.UtcNow.AddMinutes(supplier.GetTokenExpiration());
        }

        protected virtual AuthResult CreateAuthResult(User user, string token, DateTime expiration)
        {
            return new AuthResult
            {
                Token = token,
                Expiration = expiration
            };
        }
    }
}
