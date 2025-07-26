using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IMatchRepository
{
    Match GetMatchById(int matchId);
    void UpdateMatch(Match match);
}