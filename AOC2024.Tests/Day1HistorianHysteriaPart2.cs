using System.Linq;

namespace AOC2024.Tests;

public class Day1HistorianHysteriaPart2 : Day1HistorianHysteriaPart1
{
    public long ComputeSimilarity()
    {
        return Day1HistorianHysteriaPart1.Parse(Day1HistorianHysteriaPart1.Input, longs => longs.Order())
            .Similarity();
    }

    [Test]
    public void ShouldComputeTheCorrectSimilarity()
    {
        var res = ComputeSimilarity();
        Assert.That(res, Is.EqualTo(23384288));
    }
}