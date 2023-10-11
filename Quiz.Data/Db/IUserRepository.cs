using Quiz.Data.Db.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Data.Db
{
    public interface IUserRepository
    {
        public Task <User> GetUser(string login, string password);
    }
}
