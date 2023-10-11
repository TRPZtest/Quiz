using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Quiz.Configuration;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Quiz.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class QuizController : ControllerBase
    {
        public QuizController() { }


        [HttpGet]
        public async Task<string> Login([FromQuery][Required]string login, [FromQuery][Required] string password)
        {
            var userId = 14;
            var claims = new Claim[] { new Claim("userId", userId.ToString()) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)), // время действия 2 минуты
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Quizes()
        {
            return Ok();
        }
    }
}
