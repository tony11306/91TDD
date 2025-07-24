using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class MatchController(IMatchService matchService) : Controller
{
    public string UpdateMatchResult(int matchId, MatchEvent matchEvent)
    {
        var matchResult = matchService.UpdateMatchResult(matchId, matchEvent);

        return matchService.GetDisplayResult(matchResult);
    }
}

public interface IMatchRepository
{
    string GetMatchResultById(int matchId);
    void UpdateMatchResult(int matchId, string matchResult);
}

public class MatchRepository : IMatchRepository
{
    Dictionary<int, string> matches = new ();
    
    public string GetMatchResultById(int matchId)
    {
        return matches.GetValueOrDefault(matchId, "Match not found");
    }

    public void UpdateMatchResult(int matchId, string matchResult)
    {
        if (matches.ContainsKey(matchId))
        {
            matches[matchId] = matchResult;
        }
    }
}