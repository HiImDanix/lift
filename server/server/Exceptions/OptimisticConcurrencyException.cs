namespace GuessingGame.Exceptions;
using System;

public class OptimisticConcurrencyException : Exception
{
    public OptimisticConcurrencyException()
    {
    }

    public OptimisticConcurrencyException(string message)
        : base(message)
    {
    }

    public OptimisticConcurrencyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}