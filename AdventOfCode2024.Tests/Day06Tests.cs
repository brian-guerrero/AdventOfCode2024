﻿namespace AdventOfCode2024.Tests;

public class Day06Tests
{
    [Fact]
    public void SimulateGuardMovementIsCorrect()
    {
        const string input = """
                             ....#.....
                             .........#
                             ..........
                             ..#.......
                             .......#..
                             ..........
                             .#..^.....
                             ........#.
                             #.........
                             ......#...
                             """;
        var result = Day06.SimulateGuardSteps(input).DistinctPositions;
        Assert.Equal(41, result);
    }
}