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

    public static int CountSafeReportsWithTolerance(string input)
    {
        return ParseReports(input).Count(report => report.IsSafeWithTolerance);
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
    
    private IEnumerable<List<Pair>> GetTolerablePairs()
    {
        // Get all possible list of entries with one entry removed
        IEnumerable<IList<int>> entriesWithOneElementRemoved = Entries
            .Select((entry, index) => Entries.Where((_, i) => i != index)
            .ToList());
        foreach (var entries in entriesWithOneElementRemoved)
        {
            var pairs = new List<Pair>();
            for (var i = 0; i < entries.Count-1; i++)
            {
                pairs.Add(new Pair(entries[i], entries[i+1]));
            }
            yield return pairs;
        }
    }
    
    public bool IsSafe => (AllPairsAscending(GetPairs()) || AllPairsDescending(GetPairs()))
    && GetPairs().All(pair => pair.Distance is  <= 3);
    
    public bool IsSafeWithTolerance => IsSafe || 
        GetTolerablePairs().Any(pairs => (pairs.All(pair => pair.Distance is <= 3) && (AllPairsAscending(pairs) || AllPairsDescending(pairs))));

    private bool AllPairsDescending(IList<Pair> pairs)
    {
        return pairs.All(pair => pair.Difference is < 0);
    }


    private bool AllPairsAscending(IList<Pair> pairs)
    {
        return pairs.All(pair => pair.Difference is > 0);
    }
}