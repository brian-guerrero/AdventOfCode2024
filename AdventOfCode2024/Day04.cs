namespace AdventOfCode2024;

public class Day04
{
    public static CharacterMap ParseMap(string input)
    {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var map = new char[lines.Length, lines[0].Length];
        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lines[y].Length; x++)
            {
                map[y, x] = lines[y][x];
            }
        }

        return new CharacterMap(map);
    }

    public static int CountXMASes(string input)
    {
        var map = ParseMap(input);
        var count = 0;
        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                if (map[x, y] == 'X')
                {
                    if (map.IsInBounds(x + 1, y) && map[x + 1, y] == 'M')
                    {
                        if (map.IsInBounds(x + 2, y) && map[x + 2, y] == 'A')
                        {
                            if (map.IsInBounds(x + 3, y) && map[x + 3, y] == 'S')
                            {
                                count++;
                            }
                        }
                    }

                    if (map.IsInBounds(x - 1, y) && map[x - 1, y] == 'M')
                    {
                        if (map.IsInBounds(x - 2, y) && map[x - 2, y] == 'A')
                        {
                            if (map.IsInBounds(x - 3, y) && map[x - 3, y] == 'S')
                            {
                                count++;
                            }
                        }
                    }

                    if (map.IsInBounds(x, y + 1) && map[x, y + 1] == 'M')
                    {
                        if (map.IsInBounds(x, y + 2) && map[x, y + 2] == 'A')
                        {
                            if (map.IsInBounds(x, y + 3) && map[x, y + 3] == 'S')
                            {
                                count++;
                            }
                        }
                    }

                    if (map.IsInBounds(x, y - 1) && map[x, y - 1] == 'M')
                    {
                        if (map.IsInBounds(x, y - 2) && map[x, y - 2] == 'A')
                        {
                            if (map.IsInBounds(x, y - 3) && map[x, y - 3] == 'S')
                            {
                                count++;
                            }
                        }
                    }

                    if (map.IsInBounds(x + 1, y + 1) && map[x + 1, y + 1] == 'M')
                    {
                        if (map.IsInBounds(x + 2, y + 2) && map[x + 2, y + 2] == 'A')
                        {
                            if (map.IsInBounds(x + 3, y + 3) && map[x + 3, y + 3] == 'S')
                            {
                                count++;
                            }
                        }
                    }

                    if (map.IsInBounds(x - 1, y - 1) && map[x - 1, y - 1] == 'M')
                    {
                        if (map.IsInBounds(x - 2, y - 2) && map[x - 2, y - 2] == 'A')
                        {
                            if (map.IsInBounds(x - 3, y - 3) && map[x - 3, y - 3] == 'S')
                            {
                                count++;
                            }
                        }
                    }

                    if (map.IsInBounds(x - 1, y + 1) && map[x - 1, y + 1] == 'M')
                    {
                        if (map.IsInBounds(x - 2, y + 2) && map[x - 2, y + 2] == 'A')
                        {
                            if (map.IsInBounds(x - 3, y + 3) && map[x - 3, y + 3] == 'S')
                            {
                                count++;
                            }
                        }
                    }

                    if (map.IsInBounds(x + 1, y - 1) && map[x + 1, y - 1] == 'M')
                    {
                        if (map.IsInBounds(x + 2, y - 2) && map[x + 2, y - 2] == 'A')
                        {
                            if (map.IsInBounds(x + 3, y - 3) && map[x + 3, y - 3] == 'S')
                            {
                                count++;
                            }
                        }
                    }
                }
            }
        }

        return count;
    }

    public static int CountTwoMAS(string input)
    {
        var map = ParseMap(input);
        var count = 0;
        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                if (map[x, y] != 'A') continue;
                if (map.IsInBounds(x + 1, y + 1) && map.IsInBounds(x - 1, y - 1) && map.IsInBounds(x - 1, y + 1) &&
                    map.IsInBounds(x + 1, y - 1))
                {
                    if (map[x - 1, y + 1] is 'M' or 'S' && map[x + 1, y - 1] != map[x - 1, y + 1] &&
                        map[x + 1, y - 1] is 'S' or 'M' and not 'A')
                    {
                        if (map[x - 1, y - 1] is 'M' or 'S' && map[x + 1, y + 1] != map[x - 1, y - 1] &&
                            map[x + 1, y + 1]  is 'S' or 'M' and not 'A')
                        {
                            count++;
                        }
                    }
                }
            }
        }

        return count;
    }
}

public record CharacterMap(char[,] Map)
{
    public int Width => Map.GetLength(1);
    public int Height => Map.GetLength(0);
    public char this[int x, int y] => Map[y, x];

    public bool IsInBounds(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
}