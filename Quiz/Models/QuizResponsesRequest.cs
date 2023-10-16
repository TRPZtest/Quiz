using QuizApi.Data.Db.Enteties;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizApi.Models
{
    public class QuizResponsesRequest
    {
        [Required]
        public long TakeId { get; set; }
        [Required]
        public long[] OptionIds { get; set; }
    }
}
