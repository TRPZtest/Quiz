using Microsoft.EntityFrameworkCore;
using QuizApi.Data.Db.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Data.Db
{
    public class QuizApiRepository
    {
        private readonly TestingDbContext _context;

        public QuizApiRepository(TestingDbContext dbContext) 
        {
            _context = dbContext;
        }
       
        public async Task<List<Quiz>>GetQuizzesAsync(int page, int pageSize)
        {        
            var test =  _context.Quizzes.Where(x => x.Id == 3);
            var quizzes = await _context.Quizzes.AsNoTracking()               
                .Skip(page * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return quizzes;
        }

        public async Task<Quiz>GetQuizAsync(long quizId)
        {
            var quiz = await _context.Quizzes.AsNoTracking()
                .Include(x => x.Questions)
                    .ThenInclude(x => x.Options)
                .AsNoTracking()
                .FirstAsync(x => x.Id == quizId);

            return quiz;
        }

        public async Task<Response[]> GetResponses(long takeId)
        {
            var responses = await _context.Responses.Where(x => x.TakeId == takeId)
                .Include(x => x.Option)
                .AsNoTracking()
                .ToArrayAsync();

            return responses;
        }

        public async Task<Response[]> GetResponses(long takeId, long questionId)
        {
            var responses = await _context.Responses.Where(x => x.TakeId == takeId)
                .Include(x => x.Option)
                .AsNoTracking()
                .ToArrayAsync();
            
            return responses;
        }

        public async Task AddResponsesAsync(params Response[] responses)
        {
            await _context.Responses.AddRangeAsync(responses);
        }

        public async Task<long> AddTakeWithSavingAsync(Take take)
        {
            var result = await _context.Takes.AddAsync(take);
            await SaveChangesAsync();

            var insertedTakeId = result.Entity.Id;

            return insertedTakeId;
        }

        public async Task AddResultAsync(Result result)
        {
            await _context.Results.AddAsync(result);
        }

        public async Task<Result?> GetResultAsync(long takeId)
        {
            var result = await _context.Results
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TakeId == takeId);

            return result;
        }

        public async Task<int> SaveChangesAsync()
        {
            var dbChangesCount = await _context.SaveChangesAsync();

            return dbChangesCount;
        }      
    }
}
