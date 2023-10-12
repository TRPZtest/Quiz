using Microsoft.AspNetCore.Mvc;
using QuizApi.Data.Db;
using QuizApi.Data.Db.Enteties;
using QuizApi.Models;
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Services
{
    public class QuizService
    {
        private QuizApiRepository _repository;

        public QuizService(QuizApiRepository repository) 
        { 
            _repository = repository;
        }
      
        public async Task<List<Quiz>> GetQuizzesAsync(int page, int pageSize)
        {
            var quizzes = await _repository.GetQuizzesAsync(page, pageSize);
            return quizzes;
        }
       
        public async Task<long> AddTakeAsync(long quizId, long userId)
        {
            var takeId = await _repository.AddTakeWithSavingAsync(new Data.Db.Enteties.Take { QuizId = quizId, UserId = 1 });

            if (takeId == 1)
                throw new Exception("Error while adding new take");
     
            return takeId;
        }
      
        public async Task<int> AddResponsesAsync(Response[] responses)
        {
            await _repository.AddResponsesAsync(responses);
            var addedItemsCount = await _repository.SaveChangesAsync();

            if (addedItemsCount < 1)
                throw new Exception("Error while adding response");

            return addedItemsCount;
        }
       
        public async Task<int> AddResultAsync(long takeId)
        {
            var responses = await _repository.GetResponses(takeId);
            var quiz = await _repository.GetQuizAsync(takeId);

            var maxPoint = quiz.Questions.Count();
            var points = responses.Count(x => x.Option.IsCorrect == true);

            await _repository.AddResultAsync(new Result { MaxPoints = maxPoint, Points = points });
            var addedItemsCount = await _repository.SaveChangesAsync();

            if (addedItemsCount < 1)
                throw new Exception("Error while adding new result");

            return addedItemsCount;
        }
      
        public async Task<Result> GetResultAsync(long takeId)
        {
            var result = await _repository.GetResultAsync(takeId);
           
            return result;
        }
    }
}
