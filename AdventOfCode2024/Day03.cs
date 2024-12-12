using System.Text.RegularExpressions;

namespace AdventOfCode2024;

public partial class Day03
{
    
    public static IEnumerable<MulInstruction> ParseMulInstructions(string input)
    {
        var instructions = MulRegex().Matches(input);
        foreach (Match instruction in instructions)
        {
            var a = int.Parse(instruction.Groups[1].Value);
            var b = int.Parse(instruction.Groups[2].Value);
            yield return new MulInstruction(a, b);
        }
    }
    
    public static IEnumerable<MulInstruction> ParseMulInstructionsWithDoOrDontOperands(string input)
    {
        var instructions = MulWithDoOrDontRegex().Matches(input);
        var skipMul = false;
        foreach (Match instruction in instructions)
        {
            if (instruction.Groups[1].Value == "do()")
            {
                skipMul = false;
                continue;
            }
            if (instruction.Groups[1].Value == "don't()")
            {
                skipMul = true;
                continue;
            }

            if (skipMul) continue;
            var a = int.Parse(instruction.Groups[2].Value);
            var b = int.Parse(instruction.Groups[3].Value);
            yield return new MulInstruction(a, b);
        }
    }

    public static int EvaluateMulInstructionsFromCorruptedString(string input)
    {
        return ParseMulInstructions(input).Sum(instruction => instruction.Result);
    }
    
    public static int EvaluateMulInstructionsWithDoOrDontFromCorruptedString(string input)
    {
        return ParseMulInstructionsWithDoOrDontOperands(input).Sum(instruction => instruction.Result);
    }
    
    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MulRegex();
    
    [GeneratedRegex(@"(do\(\)|don't\(\)|mul\((\d+),(\d+)\))")]
    private static partial Regex MulWithDoOrDontRegex();
}

public record MulInstruction(int A, int B)
{
    public int Result => A * B;
}