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

        public async Task<User>GetUserAsync(string login, string password)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstAsync(x => x.Login == login && x.Password == x.Password);            
            return user;
        }

        public async Task<List<Quiz>> GetQuizzesAsync(int page, int pageSize)
        {
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

        public async Task AddResponsesAsync(IEnumerable<Response> responses)
        {
            await _context.Responses.AddRangeAsync(responses);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
