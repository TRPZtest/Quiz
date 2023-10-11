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
        private readonly QuizApiDbContext _context;

        public QuizApiRepository(QuizApiDbContext dbContext) 
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
    }
}
