namespace AdventOfCode2024.Tests;

public class Day01Tests
{
    private readonly static List<int> Locations1 = new List<int> { 3, 4, 2, 1, 3, 3 };
    private readonly static List<int> Locations2 = new List<int> { 4,3,5,3,9,3 };

    [Fact]
    public void PairDistancesAreCorrect()
    {
        var pairs = Day01.GetPairs(Locations1, Locations2).ToList();
        var expected = new List<int> { 2, 1, 0, 1, 2, 5 };
        Assert.Equal(expected, pairs.Select(p => p.Distance));
    }

    [Fact]
    public void CalculateDistanceIsCorrect()
    {
        var distance = Day01.CalculateDistance(Locations1, Locations2);
        Assert.Equal(11, distance);
    }
    
    [Fact]
    public void GetSimilarityScoreIsCorrect()
    {
        var score = Day01.GetSimilarityScore(Locations2, 3);
        Assert.Equal(9, score);
    }

    [Fact]
    public void CalculateSimilarityScoreIsCorrect()
    {
        var similarityScore = Day01.CalculateSimilarityScore(Locations1, Locations2);
        Assert.Equal(31, similarityScore);
    }
}

public class PairTests
{
    [Fact]
    public void DistanceIsCorrect()
    {
        var pair = new Pair(1, 3);
        Assert.Equal(2, pair.Distance);
    }
}