using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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

    [Test]
    public void awaygoal()
    {
        // Arrange
        var matchId = 1;
        var initialResult = "";
        _matchRepository.GetMatchResultById(matchId).Returns(initialResult);

        // Act
        var result = _matchService.UpdateMatchResult(matchId, MatchEvent.AwayGoal);

        // Assert
        _matchRepository.Received(1).GetMatchResultById(matchId);
        _matchRepository.Received(1).UpdateMatchResult(matchId, "A");
        result.Should().Be("A");
    }

    [Test]
    public void nextperiod()
    {
        // Arrange
        var matchId = 1;
        var initialResult = "A";
        _matchRepository.GetMatchResultById(matchId).Returns(initialResult);

        // Act
        var result = _matchService.UpdateMatchResult(matchId, MatchEvent.NextPeriod);

        // Assert
        _matchRepository.Received(1).GetMatchResultById(matchId);
        _matchRepository.Received(1).UpdateMatchResult(matchId, "A;");
        result.Should().Be("A;");
    }

    [Test]
    public void homecancel()
    {
        // Arrange
        var matchId = 1;
        var initialResult = "AH";
        _matchRepository.GetMatchResultById(matchId).Returns(initialResult);

        // Act
        var result = _matchService.UpdateMatchResult(matchId, MatchEvent.HomeCancel);

        // Assert
        _matchRepository.Received(1).GetMatchResultById(matchId);
        _matchRepository.Received(1).UpdateMatchResult(matchId, "A");
        result.Should().Be("A");
    }

    [Test]
    public void homecancel_with_semicolon()
    {
        // Arrange
        var matchId = 1;
        var initialResult = "AH;";
        _matchRepository.GetMatchResultById(matchId).Returns(initialResult);

        // Act
        var result = _matchService.UpdateMatchResult(matchId, MatchEvent.HomeCancel);

        // Assert
        _matchRepository.Received(1).GetMatchResultById(matchId);
        _matchRepository.Received(1).UpdateMatchResult(matchId, "A;");
        result.Should().Be("A;");
    }

    [Test]
    public void awaycancel()
    {
        // Arrange
        var matchId = 1;
        var initialResult = "HA";
        _matchRepository.GetMatchResultById(matchId).Returns(initialResult);

        // Act
        var result = _matchService.UpdateMatchResult(matchId, MatchEvent.AwayCancel);

        // Assert
        _matchRepository.Received(1).GetMatchResultById(matchId);
        _matchRepository.Received(1).UpdateMatchResult(matchId, "H");
        result.Should().Be("H");
    }

    [Test]
    public void awaycancel_with_semicolon()
    {
        // Arrange
        var matchId = 1;
        var initialResult = "HA;";
        _matchRepository.GetMatchResultById(matchId).Returns(initialResult);

        // Act
        var result = _matchService.UpdateMatchResult(matchId, MatchEvent.AwayCancel);

        // Assert
        _matchRepository.Received(1).GetMatchResultById(matchId);
        _matchRepository.Received(1).UpdateMatchResult(matchId, "H;");
        result.Should().Be("H;");
    }

    [TestCase("", MatchEvent.HomeCancel)]
    [TestCase("", MatchEvent.AwayCancel)]
    [TestCase("AH", MatchEvent.AwayCancel)]
    [TestCase("HA", MatchEvent.HomeCancel)]
    public void homecancel_with_unexpected_condition(string initialResult, MatchEvent homeCancel)
    {
        // Arrange
        var matchId = 1;
        _matchRepository.GetMatchResultById(matchId).Returns(initialResult);

        // Assert
        Assert.Throws<UpdateMatchResultException>(() =>
            _matchService.UpdateMatchResult(matchId, homeCancel));
    }

    [TestCase("", "0:0 (First Half)")]
    [TestCase("H", "1:0 (First Half)")]
    [TestCase("A", "0:1 (First Half)")]
    [TestCase("HA;", "1:1 (Second Half)")]
    [TestCase("HA;H", "2:1 (Second Half)")]
    public void get_display_result(string matchResult, string expected)
    {
        var displayResult = _matchService.GetDisplayResult(matchResult);
        displayResult.Should().Be(expected);
    }
}