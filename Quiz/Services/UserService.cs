using Quiz.Data.Db;
using Quiz.Data.Db.Enteties;

namespace Quiz.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository userRepository) 
        {
            _repository = userRepository;
        }
        
        public async Task<User> GetUserAsync(string user)
        {
            return new User { };
        }
    }
}
