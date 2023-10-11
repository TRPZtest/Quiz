using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Data.Db
{
    public class QuizDbcontext : DbContext
    {
        public QuizDbcontext(DbContextOptions options) : base(options) { }


    }
}
