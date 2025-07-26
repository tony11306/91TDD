using WebApplication1.Exceptions;

namespace WebApplication1.Models;

using System.Linq;

public class MatchResult(string result)
{
    private string _result = result;

    public void NextPeriod()
    {
        _result += ';';
    }

    public string GetDisplayString()
    {
        var homeScore = _result.Count(c => c == 'H');
        var awayScore = _result.Count(c => c == 'A');
        var period = _result.Contains(';') ? "Second Half" : "First Half";

        return $"{homeScore}:{awayScore} ({period})";
    }

    public override string ToString()
    {
        return _result;
    }

    public void AddGoal(char c)
    {
        _result += c;
    }

    public void CancelGoal(char c)
    {
        if (_result.EndsWith(';'))
        {
            if (_result[..^1].EndsWith(c))
            {
                _result = _result[..^2] + ';';
            }
            else
            {
                throw new UpdateMatchResultException(c == 'H' ? MatchEvent.HomeCancel : MatchEvent.AwayCancel, _result);
            }
        } 
        else if(_result.EndsWith(c))
        {
            _result = _result[..^1];
        }
        else
        {
            throw new UpdateMatchResultException(c == 'H' ? MatchEvent.HomeCancel : MatchEvent.AwayCancel, _result);
        }
    }
}