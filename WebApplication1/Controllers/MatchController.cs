using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

public class MatchController(IMatchRepository matchRepository) : Controller
{
    public string UpdateMatchResult(int matchId, MatchEvent matchEvent)
    {
        var match = matchRepository.GetMatchById(matchId);
        match.UpdateResult(matchEvent);
        matchRepository.UpdateMatch(match);
        return match.Result.GetDisplayString();
    }
}