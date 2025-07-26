namespace WebApplication1.Models;

public class Match(int id, MatchResult result)
{
    public int Id { get; } = id;
    public MatchResult Result { get; } = result;

    public void UpdateResult(MatchEvent matchEvent)
    {
        switch (matchEvent)
        {
            case MatchEvent.HomeGoal:
                Result.AddGoal('H');
                break;
            case MatchEvent.AwayGoal:
                Result.AddGoal('A');
                break;
            case MatchEvent.NextPeriod:
                Result.NextPeriod();
                break;
            case MatchEvent.HomeCancel:
                Result.CancelGoal('H');
                break;
            case MatchEvent.AwayCancel:
                Result.CancelGoal('A');
                break;
            case MatchEvent.Unknown:
            default:
                throw new ArgumentOutOfRangeException(nameof(matchEvent), $"Unsupported event: {matchEvent}");
        }
    }
}
