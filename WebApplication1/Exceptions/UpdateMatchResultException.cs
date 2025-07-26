using WebApplication1.Models;

namespace WebApplication1.Exceptions;

public class UpdateMatchResultException : Exception
{
    public UpdateMatchResultException(MatchEvent matchEvent, string originalResult)
        : base($"{matchEvent.ToString()}: {originalResult}")
    {
    }
}