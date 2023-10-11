using Microsoft.EntityFrameworkCore;
using QuizApi.Data.Db.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Data.Db
{
    public class QuizApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public QuizApiDbContext(DbContextOptions options) : base(options) { }


    }
}
