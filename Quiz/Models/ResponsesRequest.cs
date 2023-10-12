using QuizApi.Data.Db.Enteties;
using System.Text.Json.Serialization;

namespace QuizApi.Models
{
    public class ResponsesRequest
    {
        public ResponseViewModel[] Responses { get; set; }
    }

    public class ResponseViewModel : Response
    {
        [JsonIgnore]
        public override Option Option { get => base.Option; set => base.Option = value; }
        [JsonIgnore]
        public override Data.Db.Enteties.Take Take { get => base.Take; set => base.Take = value; }
        [JsonIgnore]
        public override Question Question { get => base.Question; set => base.Question = value; }
    }
}
