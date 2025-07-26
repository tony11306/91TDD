using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WebApplication1.Controllers;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Tests.Controllers;

public class MatchControllerTest
{
    private Match _match;
    private MatchController _matchController;
    private IMatchRepository _matchRepository;

    [SetUp]
    public void SetUp()
    {
        _matchRepository = Substitute.For<IMatchRepository>();
        _matchController = new MatchController(_matchRepository);
    }

    [TestCase("", MatchEvent.HomeGoal, "1:0 (First Half)", TestName = "home goal")]
    [TestCase("", MatchEvent.AwayGoal, "0:1 (First Half)", TestName = "away goal")]
    [TestCase("A", MatchEvent.NextPeriod, "0:1 (Second Half)", TestName = "next period")]
    [TestCase("AH", MatchEvent.HomeCancel, "0:1 (First Half)", TestName = "home cancel")]
    [TestCase("AH;", MatchEvent.HomeCancel, "0:1 (Second Half)", TestName = "home cancel with next period")]
    [TestCase("HA", MatchEvent.AwayCancel, "1:0 (First Half)", TestName = "away cancel")]
    [TestCase("HA;", MatchEvent.AwayCancel, "1:0 (Second Half)", TestName = "away cancel with next period")]
    public void normal_goal(string initialResult, MatchEvent matchEvent, string expected)
    {
        // Arrange
        GivenMatch(new Match(1, new MatchResult(initialResult)));

        // Act
        var updateMatchResult = _matchController.UpdateMatchResult(1, matchEvent);

        // Assert
        _matchRepository.Received(1).GetMatchById(1);
        updateMatchResult.Should().Be(expected);
    }

    [TestCase("", MatchEvent.HomeCancel)]
    [TestCase("", MatchEvent.AwayCancel)]
    [TestCase("AH", MatchEvent.AwayCancel)]
    [TestCase("HA", MatchEvent.HomeCancel)]
    public void goal_cancel_with_unexpected_condition(string initialResult, MatchEvent matchEvent)
    {
        // Arrange
        GivenMatch(new Match(1, new MatchResult(initialResult)));
        
        // Act & Assert
        Assert.Throws<UpdateMatchResultException>(() => 
            _matchController.UpdateMatchResult(1, matchEvent)
        );
    }

    private void GivenMatch(Match match)
    {
        _match = match;
        _matchRepository.GetMatchById(Arg.Any<int>()).Returns(_match);
    }
}