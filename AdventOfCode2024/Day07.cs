using System.Linq.Expressions;

namespace AdventOfCode2024;

public class Day07
{
    private static IEnumerable<CalibrationEquation> ParseCalibrationEquation(string input)
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

    public static long CalculateTotalCalibrationWithOgOpsResult(string input)
    {
        var equations = ParseCalibrationEquation(input);
        var correctlyCalibratedFunctions = equations
            .Where(e => GetCalibrationFunctions(e, OgCalibrationOperators)
                .Any(f => f == e.TestValue));
        return correctlyCalibratedFunctions.Sum(e => e.TestValue);
    }
    
    public static long CalculateTotalCalibrationWithNewOpsResult(string input)
    {
        var equations = ParseCalibrationEquation(input);
        var correctlyCalibratedFunctions = equations
            .Where(e => GetCalibrationFunctions(e, NewCalibrationOperators)
                .Any(f => f == e.TestValue));
        return correctlyCalibratedFunctions.Sum(e => e.TestValue);
    }

    private static IEnumerable<long> GetCalibrationFunctions(CalibrationEquation equation, CalibrationOperator[] operators)
    {
        var operationCombinations = GetPossibleOperatorCombinations(equation.EquationValues.Count - 1, operators);

        return operationCombinations.Select(opCombo => GenerateCalibrationFunctions(equation, opCombo));
    }

    private static long GenerateCalibrationFunctions(CalibrationEquation equation, CalibrationOperator[] opCombo)
    {
        var i = 1;
        var result =  opCombo.Aggregate(equation.EquationValues[0], (current, op) => op switch
        {
            ConcatOperator => long.Parse($"{current}{equation.EquationValues[i++]}"),
            AddOperator => current + equation.EquationValues[i++],
            MultiplyOperator => current * equation.EquationValues[i++],
            _ => throw new ArgumentOutOfRangeException()
        });
        return result;
    }

    private static IEnumerable<CalibrationOperator[]> GetPossibleOperatorCombinations(int length, CalibrationOperator[] operators)
    {
        var totalCombinations = (int)Math.Pow(operators.Length, length);
        for (var i = 0; i < totalCombinations; i++)
        {
            var combination = new CalibrationOperator[length];
            var temp = i;
            for (var j = 0; j < length; j++)
            {
                combination[j] = operators[temp % operators.Length];
                temp /= operators.Length;
            }
            yield return combination;
        }
    }


    private static readonly CalibrationOperator[] OgCalibrationOperators = [new AddOperator(), new MultiplyOperator()];
    private static readonly CalibrationOperator[] NewCalibrationOperators = [new AddOperator(), new MultiplyOperator(), new ConcatOperator()];
}

public record CalibrationOperator();
public record AddOperator() : CalibrationOperator;
public record MultiplyOperator() : CalibrationOperator;
public record ConcatOperator() : CalibrationOperator;
public record CalibrationEquation(long TestValue, List<long> EquationValues);