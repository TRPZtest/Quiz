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
        private readonly QuizApiRepository _repository;

        public QuizApiController(QuizApiRepository repository) 
        {
            _repository = repository;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromQuery]LoginRequest request)
        {
            var user = await _repository.GetUserAsync(request.Login, request.Password);

            if (user == null)
                return Unauthorized();
            
            var token = JwtHelper.GetJwt(user);

            return new LoginResponse { Token = token };
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<QuizApiesResponse>> Quizzes([FromQuery]QuizzesRequest request)
        {
            var quizzes = await _repository.GetQuizzesAsync(request.Page, request.PageSize);
            return new QuizApiesResponse { Quizzes = quizzes } ;
        }
    }
}
