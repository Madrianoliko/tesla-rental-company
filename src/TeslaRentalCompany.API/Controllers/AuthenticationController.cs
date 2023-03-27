using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeslaRentalCompany.API.Services;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public ITeslaRentalCompanyRepository Repository { get; }

        public AuthenticationController(IConfiguration configuration,
            ITeslaRentalCompanyRepository repository)
        {
            Configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            Repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(
            AuthenticationRequestBody authenticationRequestBody)
        {
            if (!await Repository.UserExistsAsync(authenticationRequestBody.UserName))
            {
                return NotFound();
            }

            var user = await Repository.ValidateCredentialsAsync(
                authenticationRequestBody.UserName,
                authenticationRequestBody.Password);
            if (user == null) { return Unauthorized(); }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(Configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("is_admin", user.IsAdmin.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                Configuration["Authentication:Issuer"],
                Configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }
        public class AuthenticationRequestBody
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public AuthenticationRequestBody(string userName, string password)
            {
                UserName = userName;
                Password = password;
            }
        }
    }
}
