﻿using GuessingGame.models;
using GuessingGame.Models;

namespace GuessingGame.Repositories
{
    public interface IQuestionRepository
    {
        Question Add(Question question);
    }
}