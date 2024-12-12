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

        return new PrintQueue(new PageOrderingRules(rules), updates);
    }

    public static int CountPrintableUpdates(string printQueueInstructions)
    {
        var queue = ParsePrintQueue(printQueueInstructions);
        return queue.ClassifyUpdates().SortedUpdates.Count();
    }

    public static int MiddlePageSumOfSortedUpdates(string printQueueInstructions)
    {
        var queue = ParsePrintQueue(printQueueInstructions);
        return queue.ClassifyUpdates().SortedUpdates.Sum(update => update.MiddlePage);
    }

    public static int MiddlePageSumOfSortedOutOfOrderUpdates(string printQueueInstructions)
    {
        var queue = ParsePrintQueue(printQueueInstructions);
        var sorted = queue.SortOutOfOrderUpdates();
        return sorted.Sum(update => update.MiddlePage);
    }
}

public record PrintQueue(PageOrderingRules Rules, IEnumerable<PageUpdates> Updates)
{
    public (IEnumerable<PageUpdates> SortedUpdates, IEnumerable<PageUpdates> OutOfOrder) ClassifyUpdates()
    {
        List<PageUpdates> correctlySorted = [];
        List<PageUpdates> incorrectlySorted = [];
        foreach (var update in Updates)
        {
            var applicableRules = Rules.GetApplicableRules(update);
            if (applicableRules.All(rule => update.FollowRule(rule)))
            {
                correctlySorted.Add(update);
            }
            else
            {
                incorrectlySorted.Add(update);
            }
        }

        return (correctlySorted, incorrectlySorted);
    }

    public IEnumerable<PageUpdates> SortOutOfOrderUpdates() => ClassifyUpdates()
        .OutOfOrder
        .Select(update => update.SortPageNumbers(Rules));
};

public class PageOrderingRules(IEnumerable<PageOrderingRule> rules) : IEnumerable<PageOrderingRule>
{
    private readonly List<PageOrderingRule> _rules = rules.ToList();

    public IEnumerator<PageOrderingRule> GetEnumerator()
    {
        return _rules.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public PageOrderingRules GetApplicableRules(PageUpdates pageUpdates)
    {
        return new PageOrderingRules(_rules.Where(rule =>
            pageUpdates.PageNumbers.Contains(rule.PrecedingPage)
            && pageUpdates.PageNumbers.Contains(rule.FollowingPage)));
    }
}

public record PageOrderingRule(int PrecedingPage, int FollowingPage);

public record PageUpdates(List<int> PageNumbers)
{
    public bool FollowRule(PageOrderingRule rule)
    {
        return PageNumbers.IndexOf(rule.PrecedingPage) < PageNumbers.IndexOf(rule.FollowingPage);
    }

    public PageUpdates SortPageNumbers(PageOrderingRules rules)
    {
        List<int> sortedPageNumbers = [];
        var applicableRules = rules.GetApplicableRules(this);
foreach (var pageNumber in PageNumbers)
{
    var latestInsertIndex = sortedPageNumbers.Count;
    foreach (var rule in applicableRules)
    {
        if (rule.FollowingPage == pageNumber)
        {
            var precedingIndex = sortedPageNumbers.IndexOf(rule.PrecedingPage);
            if (precedingIndex != -1)
            {
                latestInsertIndex = Math.Max(latestInsertIndex, precedingIndex + 1);
            }
        }
        else if (rule.PrecedingPage == pageNumber)
        {
            var followingIndex = sortedPageNumbers.IndexOf(rule.FollowingPage);
            if (followingIndex != -1)
            {
                latestInsertIndex = Math.Min(latestInsertIndex, followingIndex);
            }
        }
    }

    sortedPageNumbers.Insert(latestInsertIndex, pageNumber);
}
        return new PageUpdates(sortedPageNumbers);
    }

    public int MiddlePage => PageNumbers.Skip(PageNumbers.Count / 2).First();
};