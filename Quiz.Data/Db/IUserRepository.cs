using QuizApi.Data.Db.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApi.Data.Db
{
    public interface IUserRepository
    {
        public Task <User> GetUser(string login, string password);


    }
}
