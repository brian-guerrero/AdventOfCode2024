namespace AdventOfCode2024.Tests;

public class Day05Tests
{
    [Fact]
    public void CountPrintableUpdatesIsCorrect()
    {
        const string input = """
                             47|53
                             97|13
                             97|61
                             97|47
                             75|29
                             61|13
                             75|53
                             29|13
                             97|29
                             53|29
                             61|53
                             97|53
                             61|29
                             47|13
                             75|47
                             97|75
                             47|61
                             75|61
                             47|29
                             75|13
                             53|13

                             75,47,61,53,29
                             97,61,53,29,13
                             75,29,13
                             75,97,47,61,53
                             61,13,29
                             97,13,75,29,47
                             """;
        var result = Day05.CountPrintableUpdates(input);
        Assert.Equal(3, result);
    }

    public static IEnumerable<object[]> ExpectedMiddles()
    {
        yield return new object[] { 47, new List<int>() { 75,97,47,61,53  } };
        yield return new object[] { 29, new List<int>() { 61,  13,29 } };
        yield return new object[] { 47, new List<int>() { 97,13,75,29,47 } };
    }
    
    public static IEnumerable<object[]> ExpectedSortedList()
    {
        yield return new object[] { new List<int> {97,75,47,61,53}, new List<int>() { 75,97,47,61,53} };
        yield return new object[] { new List<int> { 61,29,13},new List<int>() { 61, 13,29 } };
        yield return new object[] { new List<int> {97,75,47,29,13}, new List<int>() { 97,13,75,29,47 } };
    }

    [Fact]
    public void MiddlePageSumOfSortedUpdatesIsCorrect()
    {
        const string input = """
                             47|53
                             97|13
                             97|61
                             97|47
                             75|29
                             61|13
                             75|53
                             29|13
                             97|29
                             53|29
                             61|53
                             97|53
                             61|29
                             47|13
                             75|47
                             97|75
                             47|61
                             75|61
                             47|29
                             75|13
                             53|13

                             75,47,61,53,29
                             97,61,53,29,13
                             75,29,13
                             75,97,47,61,53
                             61,13,29
                             97,13,75,29,47
                             """;
        var result = Day05.MiddlePageSumOfSortedUpdates(input);
        Assert.Equal(143, result);
    }

    [Theory]
    [MemberData(nameof(ExpectedMiddles))]
    public void MiddlePageOfSortedOutOrderUpdatesShouldBeExpected(int expected, List<int> pages)
    {
        const string input = """
                             47|53
                             97|13
                             97|61
                             97|47
                             75|29
                             61|13
                             75|53
                             29|13
                             97|29
                             53|29
                             61|53
                             97|53
                             61|29
                             47|13
                             75|47
                             97|75
                             47|61
                             75|61
                             47|29
                             75|13
                             53|13

                             75,47,61,53,29
                             97,61,53,29,13
                             75,29,13
                             75,97,47,61,53
                             61,13,29
                             97,13,75,29,47
                             """;
        var rules = Day05.ParsePrintQueue(input).Rules;
        var queue = new PrintQueue(rules, [new PageUpdates(pages)]);
        Assert.Equal(expected, queue.SortOutOfOrderUpdates().First().MiddlePage);
    }

    [Theory]
    [MemberData(nameof(ExpectedSortedList))]
    public void ListOfSortedOutOrderUpdatesShouldBeExpected(List<int> expected, List<int> pages)
    {
        const string input = """
                             47|53
                             97|13
                             97|61
                             97|47
                             75|29
                             61|13
                             75|53
                             29|13
                             97|29
                             53|29
                             61|53
                             97|53
                             61|29
                             47|13
                             75|47
                             97|75
                             47|61
                             75|61
                             47|29
                             75|13
                             53|13

                             75,47,61,53,29
                             97,61,53,29,13
                             75,29,13
                             75,97,47,61,53
                             61,13,29
                             97,13,75,29,47
                             """;
        var rules = Day05.ParsePrintQueue(input).Rules;
        var queue = new PrintQueue(rules, [new PageUpdates(pages)]);
        Assert.Equal(expected, queue.SortOutOfOrderUpdates().First().PageNumbers);
    }

    
    [Fact]
    public void MiddlePageSumOfSortedOutOfOrderUpdatesIsCorrect()
    {
        const string input = """
                             47|53
                             97|13
                             97|61
                             97|47
                             75|29
                             61|13
                             75|53
                             29|13
                             97|29
                             53|29
                             61|53
                             97|53
                             61|29
                             47|13
                             75|47
                             97|75
                             47|61
                             75|61
                             47|29
                             75|13
                             53|13

                             75,47,61,53,29
                             97,61,53,29,13
                             75,29,13
                             75,97,47,61,53
                             61,13,29
                             97,13,75,29,47
                             """;
        var result = Day05.MiddlePageSumOfSortedOutOfOrderUpdates(input);
        Assert.Equal(123, result);
    }
}