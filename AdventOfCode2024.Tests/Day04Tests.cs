namespace AdventOfCode2024.Tests;

public class Day04Tests
{
    [Fact]
    public void CountXmasesIsCorrect()
    {
        const string input = """
                             ....XXMAS.
                             .SAMXMS...
                             ...S..A...
                             ..A.A.MS.X
                             XMASAMX.MM
                             X.....XA.A
                             S.S.S.S.SS
                             .A.A.A.A.A
                             ..M.M.M.MM
                             .X.X.XMASX
                             """;
        var count = Day04.CountXMASes(input);
        Assert.Equal(18, count);
    }
}