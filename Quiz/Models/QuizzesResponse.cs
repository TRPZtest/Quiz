﻿using QuizApi.Data.Db.Enteties;

namespace QuizApi.Models
{
    public class QuizzesResponse
    {
        public List<Quiz> Quizzes { get; set; }
    }

    public class QuizResponse
    {
        public Quiz quiz { get; set; }
    }
}
