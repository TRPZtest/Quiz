using Azure.Core;
using QuizApi.Data.Db;
using QuizApi.Helpers;
using QuizApi.Models;

namespace QuizApi.Services
{
    public class AuthService
    {
        private readonly UserRepository _repository;

        public AuthService(UserRepository userRepository) 
        {
            _repository = userRepository;
        }
        public async Task<string> GetJwtAsync(string login, string password)
        {
            var user = await _repository.GetUserAsync(login, password);

            if (user == null)
                return string.Empty;

            var token = JwtHelper.GetJwt(user);

            return token;
        }

    }
}
