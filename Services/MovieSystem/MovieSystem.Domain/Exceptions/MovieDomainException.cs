using System;

namespace MovieSystem.Domain.Exceptions;
/// <summary>
/// Exception type for domain exceptions.
/// </summary>
public class MovieDomainException : Exception
{
    public MovieDomainException() { }

    public MovieDomainException(string message) : base(message) { }

    public MovieDomainException(string message, Exception innerException) : base(message, innerException) { }
}
