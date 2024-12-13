using System.Linq.Expressions;

namespace AdventOfCode2024;

public class Day07
{
    private static IEnumerable<CalibrationEquation> ParseCalibationEquation(string input)
    {
        var lines = input.Split("\r\n");
        foreach (var line in lines)
        {
            var equation = line.Split(":");
            var testValue = long.Parse(equation[0]);
            var equationValues = equation[1].Trim().Split(" ").Select(long.Parse).ToList();
            yield return new CalibrationEquation(testValue, equationValues);
        }
    }

    public static long CalculateTotalCalibrationResult(string input)
    {
        var equations = ParseCalibationEquation(input);
        var correctlyCalibratedFunctions = equations
            .Where(e => GenerateCalibrationFunctions(e)
                .Any(f => f() == e.TestValue));
        return correctlyCalibratedFunctions.Sum(e => e.TestValue);
    }

    private static IEnumerable<Func<long>> GenerateCalibrationFunctions(CalibrationEquation equation)
    {
        var operationCombinations = GetPossibleOperatorCombinations(equation.EquationValues.Count - 1);

        return operationCombinations.Select(opCombo => GenerateCalibrationFunctions(equation, opCombo));
    }

    private static Func<long> GenerateCalibrationFunctions(CalibrationEquation equation, char[] opCombo)
    {
        Expression expression = Expression.Constant(equation.EquationValues[0]);
        var i = 1;
        expression = opCombo.Aggregate(expression, (current, op) => op switch
        {
            '+' => Expression.Add(current, Expression.Constant(equation.EquationValues[i++])),
            '*' => Expression.Multiply(current, Expression.Constant(equation.EquationValues[i++])),
            _ => throw new ArgumentOutOfRangeException()
        });

        return Expression.Lambda<Func<long>>(expression).Compile();
    }

    private static IEnumerable<char[]> GetPossibleOperatorCombinations(int length)
    {
        for (var i = 0; i < Math.Pow(CalibrationOperators.Length, length); i++)
        {
            var binary = Convert.ToString(i, 2).PadLeft(length, '0');
            yield return binary.Select(c => c == '0' ? '+' : '*').ToArray();
        }
    }


    private static readonly char[] CalibrationOperators = ['+', '*'];
}

public record CalibrationEquation(long TestValue, List<long> EquationValues);