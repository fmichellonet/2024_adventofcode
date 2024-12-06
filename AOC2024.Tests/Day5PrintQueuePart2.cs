using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2024.Tests;

public class Day5PrintQueuePart2
{

    [Test]
    public void ShouldComputeTheCorrectTotalForExample()
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
        var (orderingRules, updates) = Day5PrintQueuePart1.Parse(input);
        var total = Day5PrintQueuePart1.Validate(orderingRules, updates)
            .Where(x => !x.IsValid)
            .Select(x => Correct(x, orderingRules, 1))
            .Sum(x => x.MiddlePage);

        Assert.That(total, Is.EqualTo(123));
    }

    [Test]
    public void ShouldComputeTheCorrectTotal()
    {
        var (orderingRules, updates) = Day5PrintQueuePart1.Parse(Day5PrintQueuePart1.Input);
        var total = Day5PrintQueuePart1.Validate(orderingRules, updates)
            .Where(x => !x.IsValid)
            .Select(x => Correct(x, orderingRules, 1))
            .Sum(x => x.MiddlePage);

        Assert.That(total, Is.EqualTo(6336));
    }

    private static Day5PrintQueuePart1.Update Correct(Day5PrintQueuePart1.Update update, Dictionary<int, IEnumerable<int>> orderingRules, int startIndex)
    {
        if (Day5PrintQueuePart1.Validate(orderingRules, update.PageNumbers).IsValid)
        {
            return update;
        }

        var newPages = update.PageNumbers.ToList();

        for (var pageIdx = startIndex; pageIdx < newPages.Count; pageIdx++)
        {
            var currentPage = newPages.ElementAt(pageIdx);
            var previousPage = newPages.ElementAt(pageIdx - 1);
            if (!orderingRules.TryGetValue(currentPage, out var rule))
            {
                continue;
            }

            if (!rule.Contains(previousPage))
            {
                continue;
            }
            newPages[pageIdx] = previousPage;
            newPages[pageIdx -1] = currentPage;
            return Correct(new Day5PrintQueuePart1.Update(newPages, false), orderingRules, Math.Max(1, pageIdx - 1) );
        }

        throw new InvalidProgramException();
    }
}