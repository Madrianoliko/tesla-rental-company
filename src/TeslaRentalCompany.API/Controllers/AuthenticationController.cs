using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }
        private class User
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public User(int userId, string userName, string firstName, string lastName)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
            }
        }

        public AuthenticationController(IConfiguration configuration)
        {
            Configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(
            AuthenticationRequestBody authenticationRequestBody)
        {
            var user = ValidateCredentials(
                authenticationRequestBody.UserName,
                authenticationRequestBody.Password);
            if (user == null) { return Unauthorized(); }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(Configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));

            var jwtSecurityToken = new JwtSecurityToken(
                Configuration["Atuhentication:Issuer"],
                Configuration["Atuhentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);
            
            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private User ValidateCredentials(string? userName, string? password)
        {
            // sprawdzenie poprawnosci credensials
            // jezeli poprawne to zwróć użytkownika
            return new User(
                1,
                userName ?? "",
                "Adrian",
                "Malik");
        }
    }
}
