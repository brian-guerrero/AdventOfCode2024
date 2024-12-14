using Microsoft.VisualBasic;

namespace AdventOfCode2024;

public class Day08
{
    public static (Antenna[], Coordinates MapBounds) ParseAntennaMap(string input)
    {
        var antennas = new List<Antenna>();
        var lines = input.Split("\r\n");
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i].AsSpan();
            for (var j = 0; j < line.Length; j++)
            {
                if (char.IsLetter(line[j]) || char.IsDigit(line[j]))
                {
                    antennas.Add(new Antenna(line[j], new Coordinates(j, i)));
                }
            }
        }

        return (antennas.ToArray(), new Coordinates(lines[0].Length, lines.Length));
    }


    private static Coordinates GetAntinodeCoordinates(Antenna antenna, Antenna otherAntenna)
    {
        var x = antenna.Position.X < otherAntenna.Position.X ? antenna.Position.X - (otherAntenna.Position.X - antenna.Position.X) : antenna.Position.X  + (antenna.Position.X - otherAntenna.Position.X);
        var y = antenna.Position.Y > otherAntenna.Position.Y ? antenna.Position.Y - (otherAntenna.Position.Y - antenna.Position.Y) : antenna.Position.Y  + (antenna.Position.Y - otherAntenna.Position.Y);

        return new Coordinates(x, y);
    }
    
    private static IEnumerable<Antinode> FindAntinodes(string input)
    {
        var (antennas, bounds) = ParseAntennaMap(input);
        
        foreach(var antennaGroup in antennas.GroupBy(a => a.Frequency))
        {
            foreach(var antenna in antennaGroup.ToArray())
            {
                foreach (var antinode in antennaGroup.Where(a => a != antenna).Select(otherAntenna => new Antinode(GetAntinodeCoordinates(antenna, otherAntenna))))
                {
                    if(antinode.Position.X < 0 || antinode.Position.Y < 0 || antinode.Position.X >= bounds.X || antinode.Position.Y >= bounds.Y)
                        continue;
                    yield return antinode;
                };
            }
        }

    }
    
    public static int CalculateAntinodeCount(string input)
    {
        return FindAntinodes(input).DistinctBy(x => x.Position).Count();
    }
    
    
    
}


public record Antenna(char Frequency, Coordinates Position);

public record Antinode(Coordinates Position) : Antenna('#', Position);