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
        } else if (matchEvent == MatchEvent.AwayGoal)
        {
            matchResult += 'A';
        } else if (matchEvent == MatchEvent.NextPeriod)
        {
            matchResult += ';';
        } else if (matchEvent == MatchEvent.HomeCancel)
        {
            if (matchResult.Length > 0 && matchResult[^1] == 'H')
            {
                matchResult = matchResult[..^1]; // Remove last 'H'
            } else if (matchResult.Length > 0 && matchResult[^1] == ';' && matchResult.Length > 1 && matchResult[^2] == 'H')
            {
                // remove last 'H' before ';'
                matchResult = matchResult[..^2] + ';';
            }
        } else if (matchEvent == MatchEvent.AwayCancel)
        {
            if (matchResult.Length > 0 && matchResult[^1] == 'A')
            {
                matchResult = matchResult[..^1]; // Remove last 'A'
            } else if (matchResult.Length > 0 && matchResult[^1] == ';' && matchResult.Length > 1 && matchResult[^2] == 'A')
            {
                // remove last 'A' before ';'
                matchResult = matchResult[..^2] + ';';
            }
        }

        matchRepository.UpdateMatchResult(matchId, matchResult);
        return matchResult;
    }
}