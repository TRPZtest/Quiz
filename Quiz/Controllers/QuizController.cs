using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizApi.Configuration;
using QuizApi.Data.Db;
using QuizApi.Data.Db.Enteties;
using QuizApi.Helpers;
using QuizApi.Models;
using QuizApi.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService) 
        {
            _quizService = quizService;           
        }
     
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<QuizApiesResponse>> Quizzes([FromQuery][Required]int page, [FromQuery][Required]int pageSize)
        {
            var quizzes = await _quizService.GetQuizzesAsync(page, pageSize);

            return new QuizApiesResponse { Quizzes = quizzes };
        }

        [HttpPost]
        public async Task<ActionResult<TakePostResponse>> Take([FromQuery]long quizId)
        {
            var takeId = await _quizService.AddTakeAsync(quizId, User.GetUserId());
         
            return new TakePostResponse { TakeId = takeId };
        }

        [HttpPost]
        public async Task<ActionResult> Responses([FromBody]ResponsesRequest request)
        {
            await _quizService.AddResponsesAsync(request.Responses);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Result(ResultPostRequest request)
        {
            await _quizService.AddResultAsync(request.TakeId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Result>> Result([FromQuery]long takeId)
        {
            var result = await _quizService.GetResultAsync(takeId);

            if (result == null)
                return NotFound();

            return result;
        }
    }
}
