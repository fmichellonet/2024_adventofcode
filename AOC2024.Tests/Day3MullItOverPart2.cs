using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2024.Tests;

public partial class Day3MullItOverPart2
{
    [GeneratedRegex(@"(mul\((?'left'\d{1,3}),(?'right'\d{1,3})\))|(do\(\))|(don't\(\))")]
    private static partial Regex MulDoDontRegex();

    private static IEnumerable<Statement> Parse(string input)
    {
        return MulDoDontRegex().Matches(input)
            .Select(Map)
            .ToArray();
    }

    private static Statement Map(Match match)
    {
        return match switch
        {
            { Value: var v} when v.StartsWith("mul") => new Multiplication(int.Parse(match.Groups["left"].Value), int.Parse(match.Groups["right"].Value)),
            { Value: "do()" } => new Do(),
            { Value: "don't()" } => new DoNot(),
            _ => throw new ArgumentOutOfRangeException(nameof(match), match, null)
        };
    }

    private static long Compute(IReadOnlyCollection<Statement> statements)
    {
        var enabled = true;
        long total = 0;
        for (var i = 0; i < statements.Count(); i++)
        {
            var currentElement = statements.ElementAt(i);
            switch (currentElement)
            {
                case DoNot:
                    enabled = false;
                    break;
                case Do:
                    enabled = true;
                    break;
                case Multiplication mul when enabled:
                    total += mul.Result;
                    break;
            }
        }

        return total;
    }

    [Test]
    public void ShouldComputeTheCorrectTotal()
    {
        var operations = Day3MullItOverPart2.Parse(Day3MullItOverPart1.Input);
        var result = Day3MullItOverPart2.Compute(operations.ToArray());

        Assert.That(result, Is.EqualTo(87163705));
    }

    private abstract record Statement();

    private record Multiplication(long Left, long Right) : Statement
    {
        public long Result => Left * Right;
    }
    private record Do() : Statement;
    private record DoNot() : Statement;
}