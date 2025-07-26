using WebApplication1.Exceptions;

namespace WebApplication1.Models;

using System.Linq;

public class MatchResult(string result)
{
    public void NextPeriod()
    {
        result += ';';
    }

    public string GetDisplayString()
    {
        var homeScore = result.Count(c => c == 'H');
        var awayScore = result.Count(c => c == 'A');
        var period = result.Contains(';') ? "Second Half" : "First Half";

        return $"{homeScore}:{awayScore} ({period})";
    }

    public override string ToString()
    {
        return result;
    }

    public void AddGoal(char c)
    {
        result += c;
    }

    public void CancelGoal(char c)
    {
        if (result.EndsWith(';'))
        {
            if (result[..^1].EndsWith(c))
            {
                result = result[..^2] + ';';
            }
            else
            {
                throw new UpdateMatchResultException(c == 'H' ? MatchEvent.HomeCancel : MatchEvent.AwayCancel, result);
            }
        } 
        else if(result.EndsWith(c))
        {
            result = result[..^1];
        }
        else
        {
            throw new UpdateMatchResultException(c == 'H' ? MatchEvent.HomeCancel : MatchEvent.AwayCancel, result);
        }
    }
}