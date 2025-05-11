namespace SigmaBank.Exceptions;

internal class InvalidTokenException(
    string? message = null,
    Exception? innerException = null) : Exception(message, innerException)
{ }