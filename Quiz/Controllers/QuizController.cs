using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Configuration;
using QuizApi.Data.Db;
using QuizApi.Helpers;
using QuizApi.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class QuizApiController : ControllerBase
    {
        public QuizApiController() { }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, [FromServices]QuizApiRepository repository)
        {
            var user = await repository.GetUserAsync(request.Login, request.Password);

            if (user == null)
                return Unauthorized();
            
            var token = JwtHelper.GetJwt(user);

            return new LoginResponse { Token = token };
        }

        [HttpGet]     
        public async Task<IActionResult> Quizzes([FromServices] QuizApiRepository repository)
        {
        
            return Ok();
        }


    }
}
