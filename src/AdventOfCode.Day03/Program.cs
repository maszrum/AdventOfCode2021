var reader = new InputFileReader("input.txt");
var lines = await reader.ReadAllLines();

/*
 * Part one
 */

var linesToColumnsConverter = new LinesToColumnsConverter();
var columns = linesToColumnsConverter.Convert(lines);

var gammaBits = columns
    .Select(column =>
    {
        var ones = column.CountOnes();
        var zeros = column.Length - ones;
        return zeros > ones ? '0' : '1';
    })
    .MakeString();

var epsilonBits = gammaBits.Negate();

var gamma = Convert.ToInt32(gammaBits, 2);
var epsilon = Convert.ToInt32(epsilonBits, 2);

var powerConsumption = gamma * epsilon;

Console.WriteLine($"Power consumption of the submarine is {powerConsumption} units.");

/*
 * Part two
 */

var oxygen = FindRating(lines, (zeros, ones) => ones >= zeros ? '1' : '0');
var co2 = FindRating(lines, (zeros, ones) => ones < zeros ? '1' : '0');

var lifeSupportingRating = oxygen * co2;

Console.WriteLine($"Life supporting rate of the submarine is: {lifeSupportingRating}.");

int FindRating(IReadOnlyList<string> inputLines, Func<int, int, char> expectedBitFunc)
{
    var index = 0;
    var converter = new LinesToColumnsConverter();

    while (inputLines.Count > 1)
    {
        var column = converter.GetSingleColumn(inputLines, index);
        var ones = column.CountOnes();
        var zeros = column.Length - ones;
    
        var expectedBit = expectedBitFunc(zeros, ones);
    
        inputLines = inputLines
            .Where(l => l[index] == expectedBit)
            .ToArray();
    
        index++;
    }
    
    return Convert.ToInt32(inputLines[0], 2);
}