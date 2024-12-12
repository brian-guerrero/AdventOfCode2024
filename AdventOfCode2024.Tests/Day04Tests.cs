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
    
    [Fact]
    public void CountTwoMASesIsCorrect()
    {
        const string input = """
                             .M.S......
                             ..A..MSMS.
                             .M.S.MAA..
                             ..A.ASMSM.
                             .M.S.M....
                             ..........
                             S.S.S.S.S.
                             .A.A.A.A..
                             M.M.M.M.M.
                             ..........
                             """;
        var count = Day04.CountTwoMAS(input);
        Assert.Equal(9, count);
    }
}