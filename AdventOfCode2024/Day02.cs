namespace AdventOfCode2024;

public class Day02
{
    
    public static IEnumerable<Report> ParseReports(string input)
    {
        return input.Split('\n').Select(report => new Report(report.Split(' ').Select(int.Parse).ToList()));
    }
    
    public static int CountSafeReports(string input)
    {
        return ParseReports(input).Count(report => report.IsSafe);
    }
}

public record Report(IList<int> Entries)
{
    private List<Pair> GetPairs()
    {
        var pairs = new List<Pair>();
        for (var i = 0; i < Entries.Count-1; i++)
        {
                pairs.Add(new Pair(Entries[i], Entries[i+1]));
        }
        return pairs;
    }
    
    public bool IsSafe => (AllPairsAscending() || AllPairsDescending())
    && GetPairs().All(pair => pair.Distance is  <= 3);

    private bool AllPairsDescending()
    {
        return GetPairs().All(pair => pair.Difference is < 0);
    }

    private bool AllPairsAscending()
    {
        return GetPairs().All(pair => pair.Difference is > 0);
    }
}