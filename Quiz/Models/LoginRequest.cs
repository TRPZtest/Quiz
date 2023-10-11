using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class LoginRequest
    {
        [Required]
        [FromQuery]
        public string Login { get; set; }
        [Required]
        [FromQuery]
        public string Password { get; set; }
    }
}
