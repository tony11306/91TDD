using WebApplication1.Models;

namespace WebApplication1.Controllers;

public interface IMatchService
{
    string UpdateMatchResult(int matchId, MatchEvent matchEvent);
}

public class MatchService(IMatchRepository matchRepository) : IMatchService
{
    public string UpdateMatchResult(int matchId, MatchEvent matchEvent)
    {
        var matchResult = matchRepository.GetMatchResultById(matchId);

        if (matchEvent == MatchEvent.HomeGoal)
        {
            matchResult += 'H';
        }

        matchRepository.UpdateMatchResult(matchId, matchResult);
        return matchResult;
    }
}