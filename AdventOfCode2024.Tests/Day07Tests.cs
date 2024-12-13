namespace AdventOfCode2024.Tests;

public class Day07Tests
{
    [Fact]
    public void CalculateTotalCalibrationResultWithOgOpsIsCorrect()
    {
        const string input = """
                             190: 10 19
                             3267: 81 40 27
                             83: 17 5
                             156: 15 6
                             7290: 6 8 6 15
                             161011: 16 10 13
                             192: 17 8 14
                             21037: 9 7 18 13
                             292: 11 6 16 20
                             """;
        var result=  Day07.CalculateTotalCalibrationWithOgOpsResult(input);
        Assert.Equal(3749, result);
    }
    
    [Fact]
    public void CalculateTotalCalibrationResultWithNewOpsIsCorrect()
    {
        const string input = """
                             190: 10 19
                             3267: 81 40 27
                             83: 17 5
                             156: 15 6
                             7290: 6 8 6 15
                             161011: 16 10 13
                             192: 17 8 14
                             21037: 9 7 18 13
                             292: 11 6 16 20
                             """;
        var result=  Day07.CalculateTotalCalibrationWithNewOpsResult(input);
        Assert.Equal(11387, result);
    }
}