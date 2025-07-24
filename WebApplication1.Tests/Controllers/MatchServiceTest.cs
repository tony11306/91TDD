using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Tests.Controllers;

public class MatchServiceTest
{
    private IMatchRepository _matchRepository;
    private IMatchService _matchService;

    [SetUp]
    public void SetUp()
    {
        _matchRepository = Substitute.For<IMatchRepository>();
        _matchService = new MatchService(_matchRepository);
    }

    [Test]
    public void homegoal()
    {
        // Arrange
        var matchId = 1;
        var initialResult = "";
        _matchRepository.GetMatchResultById(matchId).Returns(initialResult);

        // Act
        var result = _matchService.UpdateMatchResult(matchId, MatchEvent.HomeGoal);

        // Assert
        _matchRepository.Received(1).GetMatchResultById(matchId);
        _matchRepository.Received(1).UpdateMatchResult(matchId, "H");
        result.Should().Be("H");
    }
}