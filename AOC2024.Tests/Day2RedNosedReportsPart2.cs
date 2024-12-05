using System.Linq;

namespace AOC2024.Tests;

public class Day2RedNosedReportsPart2 : Day2RedNosedReportsPart1
{
    public static long ComputeSafeReportWithDampener()
    {
        var count = Parse(Input)
            .Select(x => x.Visit(1))
            .Count(x => x is { IsSafe: true });

        return count;
    }

    [Test]
    public void ShouldAcceptNotIncreasingSequenceOnLastElement()
    {
        var report = new Report([60, 61, 62, 64, 64]);
        var res = report.Visit(1);
        Assert.That(res.IsSafe, Is.True);
        Assert.That(res.ErrorCount, Is.EqualTo(1));
    }

    [Test]
    public void ShouldRefuseTwoErrors()
    {
        var report = new Report([89, 91, 92, 95, 98, 95, 96, 95]);
        var res = report.Visit(1);
        Assert.That(res.IsSafe, Is.False);
    }


    [Test]
    public void SafeWithoutRemovingAnything_1()
    {
        var report = new Report([7, 6, 4, 2, 1]);
        var res = report.Visit(1);
        Assert.That(res.IsSafe, Is.True);
    }

    [Test]
    public void UnsafeRegardlessOfWhichLevelIsRemoved_1()
    {
        var report = new Report([1, 2, 7, 8, 9]);
        var res = report.Visit(1);
        Assert.That(res.IsSafe, Is.False);
    }

    [Test]
    public void UnsafeRegardlessOfWhichLevelIsRemoved_2()
    {
        var report = new Report([9, 7, 6, 2, 1]);
        var res = report.Visit(1);
        Assert.That(res.IsSafe, Is.False);
    }

    [Test]
    public void SafeByRemovingTheSecondLevel_3()
    {
        var report = new Report([1, 3, 2, 4, 5]);
        var res = report.Visit(1);
        Assert.That(res.IsSafe, Is.True);
        Assert.That(res.ErrorCount, Is.EqualTo(1));
    }

    [Test]
    public void SafeByRemovingTheThirdLevel_4()
    {
        var report = new Report([8, 6, 4, 4, 1]);
        var res = report.Visit(1);
        Assert.That(res.IsSafe, Is.True);
        Assert.That(res.ErrorCount, Is.EqualTo(1));
    }

    [Test]
    public void SafeWithoutRemovingAnything_2()
    {
        var report = new Report([1, 3, 6, 7, 9]);
        var res = report.Visit(1);
        Assert.That(res.IsSafe, Is.True);
    }

    [Test]
    public void ShouldComputeTheCorrectSafeCount()
    {
        var res = ComputeSafeReportWithDampener();
        Assert.That(res, Is.EqualTo(566));
    }

}