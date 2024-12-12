using System.Collections;

namespace AdventOfCode2024;

public class Day05
{
    public static PrintQueue ParsePrintQueue(string printQueueInstructions)
    {
        var sections = printQueueInstructions.Split("\r\n\r\n");

        var rules = sections[0].Split("\r\n")
            .Select(rule => rule.Split("|"))
            .Select(rule => new PageOrderingRule(int.Parse(rule[0]), int.Parse(rule[1])));

        var updates = sections[1].Split("\r\n")
            .Select(update => update.Split(","))
            .Select(update => new PageUpdates(update.Select(int.Parse).ToList()));

        return new PrintQueue(rules, updates);
    }

    public static int CountPrintableUpdates(string printQueueInstructions)
    {
        var queue = ParsePrintQueue(printQueueInstructions);
        return queue.PrintableUpdates().Count();
    }
    
    public static int MiddlePageSumOfPrintableUpdates(string printQueueInstructions)
    {
        var queue = ParsePrintQueue(printQueueInstructions);
        return queue.PrintableUpdates().Sum(update => update.MiddlePage);
    }
}

public record PrintQueue(IEnumerable<PageOrderingRule> Rules, IEnumerable<PageUpdates> Updates)
{
    public IEnumerable<PageUpdates> PrintableUpdates()
    {
        foreach(var update in Updates)
        {
            var applicableRules = Rules.Where(rule => update.PageNumbers.Contains(rule.PrecedingPage) && update.PageNumbers.Contains(rule.FollowingPage));
            if(applicableRules.All(rule => update.FollowRule(rule)))
            {
                yield return update;
            }
        }
    }
};

public record PageOrderingRule(int PrecedingPage, int FollowingPage);


public record PageUpdates(List<int> PageNumbers)
{
    public bool FollowRule(PageOrderingRule rule)
    {
        return PageNumbers.IndexOf(rule.PrecedingPage) < PageNumbers.IndexOf(rule.FollowingPage);
    }

    public int MiddlePage => PageNumbers.Skip(PageNumbers.Count/2).First();
};

