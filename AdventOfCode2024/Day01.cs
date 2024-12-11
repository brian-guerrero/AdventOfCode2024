namespace AdventOfCode2024;

public class Day01
{

    public static int GetSimilarityScore(List<int> locations, int locationId)
    {
        return locationId * locations.Count(x => x == locationId);
    }

    public static IEnumerable<Pair> GetPairs(List<int> locations1, List<int> locations2)
    {
        for (var i = 0; i < locations1.Count; i++)
        {
            yield return new Pair(locations1.Order().ToList()[i], locations2.Order().ToList()[i]);
        }
    }

    public static int CalculateDistance(List<int> locations1, List<int> locations2)
    {
        return GetPairs(locations1, locations2).Sum(p => p.Distance);
    }
    
    public static int CalculateSimilarityScore(List<int> locations1, List<int> locations2)
    {
        var sum = 0;
        foreach (var (locationId, count) in locations1.CountBy(x => x))
        {
            sum += count * GetSimilarityScore(locations2, locationId);
        }

        return sum;
    }
}

public record Pair(int X, int Y)
{
    public int Difference => X - Y;
    public int Distance => Math.Abs(Difference);
}