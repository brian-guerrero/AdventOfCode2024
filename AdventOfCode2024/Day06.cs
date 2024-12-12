namespace AdventOfCode2024;

public class Day06
{
    
    public static bool IsInBounds(int x, int y, MapObject[,] map)
    {
        return x >= 0 && x < map.GetLength(1) && y >= 0 && y < map.GetLength(0);
    }
    public static MapObject[,] ParseLabMap(string input)
    {
        var lines = input.Split("\r\n");
        var map = new MapObject[lines.Length, lines[0].Length];
        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lines[0].Length; x++)
            {
                map[y, x] = lines[y][x] switch
                {
                    '.' => new EmptySpace(),
                    '#' => new Obstacle(),
                    '^' or '>' or 'v' or '<' => new Guard(x, y),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        return map;
    }
    
    public static (int Steps, int DistinctPositions) SimulateGuardSteps(string input)
    {
        var map = ParseLabMap(input);
        var guard = map.OfType<Guard>().Single();
        var direction = Direction.Up;
        var steps = 0;
        var yOffset = 0;
        var xOffset = 0;
        List<Coordinates> visited = new();
        visited.Add(guard.Position);
        while(IsInBounds(guard.Position.X + xOffset, guard.Position.Y  + yOffset, map))
        {
            yOffset = Direction.Up == direction ? -1 : Direction.Down == direction ? 1 : 0;
            xOffset = Direction.Left == direction ? -1 : Direction.Right == direction ? 1 : 0;

            if (map[guard.Position.Y + yOffset, guard.Position.X + xOffset] is Obstacle)
            {
                direction = (Direction)(((int)direction + 1) % 4);
            }
            else
            {
                guard = direction switch
                {
                    Direction.Up => new Guard(guard.Position.X, guard.Position.Y - 1),
                    Direction.Right => new Guard(guard.Position.X + 1, guard.Position.Y),
                    Direction.Down => new Guard(guard.Position.X, guard.Position.Y + 1),
                    Direction.Left => new Guard(guard.Position.X - 1, guard.Position.Y),
                    _ => guard
                };
                steps++;
                visited.Add(guard.Position);
            }
        }
        
        return (steps, visited.Distinct().Count());
    }
}

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}


public record MapObject;

public record Coordinates(int X, int Y);

public record Guard(Coordinates Position) : MapObject
{
    public Guard(int x, int y) : this(new Coordinates(x, y))
    {
    }
};
public record Obstacle : MapObject;
public record EmptySpace : MapObject;