using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly Dictionary<int, Match> _matches = new ();
    
    public Match GetMatchById(int matchId)
    {
        if (_matches.TryGetValue(matchId, out var match))
        {
            return match;
        }
        throw new KeyNotFoundException($"Match with ID {matchId} does not exist.");
    }

    public void UpdateMatch(Match match)
    {
        if (_matches.ContainsKey(match.Id))
        {
            _matches[match.Id] = match;
        }
        else
        {
            throw new KeyNotFoundException($"Match with ID {match.Id} does not exist.");
        }
    }
}