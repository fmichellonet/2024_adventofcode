using System.Collections.Generic;
using System.Linq;

namespace AOC2024.Tests;

public class Day4CeresSearchPart2
{
    private const string MAS = nameof(MAS);
    private const string SAM = nameof(SAM);

    public static long CountXmas(char[,] charList)
    {
        long count = 0;
        long verticalMaxLength = charList.GetLength(0);
        long horizontalMaxLength = charList.GetLength(1);
        for (var i = 1; i < verticalMaxLength - 1; i++)
        {
            for (var j = 1; j < horizontalMaxLength - 1; j++)
            {
                if (charList[i, j] != 'A')
                {
                    continue;
                }

                var allVariations =
                    ReadDiagonalRight(charList, i - 1, j - 1)
                        .Concat(ReadDiagonalLeft(charList, i - 1, j +1));

                if (allVariations.Count(x => x is MAS or SAM) == 2)
                {
                    count++;
                }
            }
        }
        return count;
    }

    private static IEnumerable<string> ReadDiagonalRight(char[,] charList, int i, int j)
    {
        var arr = Enumerable.Range(0, MAS.Length)
            .Select(index => charList[i + index, j + index])
            .ToArray();

        return [new string(arr)];
    }

    private static IEnumerable<string> ReadDiagonalLeft(char[,] charList, int i, int j)
    {

        var arr = Enumerable.Range(0, MAS.Length)
            .Select(index => charList[i + index, j - index])
            .ToArray();

        return [new string(arr)];
    }

    [Test]
    public void ShouldComputeTheCorrectTotalForExample()
    {
        var input = """
                    MMMSXXMASM
                    MSAMXMSMSA
                    AMXSXMAAMM
                    MSAMASMSMX
                    XMASAMXAMM
                    XXAMMXXAMA
                    SMSMSASXSS
                    SAXAMASAAA
                    MAMMMXMMMM
                    MXMXAXMASX
                    """;
        var operations = Day4CeresSearchPart1.Parse(input);
        var result = Day4CeresSearchPart2.CountXmas(operations);

        Assert.That(result, Is.EqualTo(9));
    }

    [Test]
    public void ShouldComputeTheCorrectTotal()
    {
        var operations = Day4CeresSearchPart1.Parse(Day4CeresSearchPart1.Input);
        var result = Day4CeresSearchPart2.CountXmas(operations);

        Assert.That(result, Is.EqualTo(1950));
    }
}