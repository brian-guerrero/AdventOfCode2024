﻿namespace AdventOfCode2024.Tests;

public class Day08Tests
{
    
    [Fact]
    public void ParseAntennaMap_ShouldReturnAntennas()
    {
        var input = """
                    ............
                    ........0...
                    .....0......
                    .......0....
                    ....0.......
                    ......A.....
                    ............
                    ............
                    ........A...
                    .........A..
                    ............
                    ............
                    """;
        
        var antinodes = Day08.CalculateAntinodeCount(input);
        Assert.Equal(14, antinodes);
    }
}