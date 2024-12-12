namespace AdventOfCode2024.Tests;

public class Day02Tests
{
    [Theory]
    [InlineData("7 6 4 2 1", true)]
    [InlineData("1 2 7 8 9", false)]
    [InlineData("9 7 6 2 1", false)]
    [InlineData("1 3 2 4 5", false)]
    [InlineData("8 6 4 4 1", false)]
    [InlineData("1 3 6 7 9", true)]
    public void AreReportLevelsSafe(string reportLevels, bool expected)
    {
        var report = Day02.ParseReports(reportLevels).First();
        Assert.Equal(expected, report.IsSafe);
    }
    
    [Fact]
    public void CountSafeReportsIsCorrect()
    {
        const string input = """
                             7 6 4 2 1
                             1 2 7 8 9
                             9 7 6 2 1
                             1 3 2 4 5
                             8 6 4 4 1
                             1 3 6 7 9
                             """;
        var count = Day02.CountSafeReports(input);
        Assert.Equal(2, count);
    }
    
    [Fact]
    public void CountSafeReportsWithToleranceIsCorrect()
    {
        const string input = """
                             7 6 4 2 1
                             1 2 7 8 9
                             9 7 6 2 1
                             1 3 2 4 5
                             8 6 4 4 1
                             1 3 6 7 9
                             """;
        var count = Day02.CountSafeReportsWithTolerance(input);
        Assert.Equal(4, count);
    }
}