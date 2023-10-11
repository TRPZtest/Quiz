using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class QuizzesRequest
    {
        [Required]
        [FromQuery]
        public int Page { get; set; }
        [Required]
        [FromQuery]
        public int PageSize { get; set; }
    }
}
