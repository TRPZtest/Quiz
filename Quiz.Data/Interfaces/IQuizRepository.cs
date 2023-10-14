using QuizApi.Data.Db.Enteties;

namespace QuizApi.Data.Interfaces
{
    public interface IQuizRepository
    {
        Task AddResponsesAsync(params Response[] responses);
        Task AddResultAsync(Result result);
        Task<long> AddTakeWithSavingAsync(Take take);
        Task<Quiz> GetQuizAsync(long quizId);
        Task<List<Quiz>> GetQuizzesAsync();
        Task<Response[]> GetResponses(long takeId);
        Task<Response[]> GetResponses(long takeId, long questionId);
        Task<Result?> GetResultAsync(long takeId);
        Task<int> SaveChangesAsync();
    }
}