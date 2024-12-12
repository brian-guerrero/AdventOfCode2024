namespace AdventOfCode2024.Tests;

public class Day03Tests
{
    [Theory]
    [InlineData("mul(44,46)", 2024)]
    [InlineData("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))", 161)]
    public void MulInstructionsAreParsedCorrectly(string input, int expected)
    {
        var results = Day03.EvaluateMulInstructionsFromCorruptedString(input);
        Assert.Equal(expected, results);
    }
    
    [Theory]
    [InlineData("mul(44,46)", 2024)]
    [InlineData("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))", 48)]
    public void MulInstructionsWithDoOrDontAreParsedCorrectly(string input, int expected)
    {
        var results = Day03.EvaluateMulInstructionsWithDoOrDontFromCorruptedString(input);
        Assert.Equal(expected, results);
    }
}