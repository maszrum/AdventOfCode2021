var reader = new InputFileReader("input.txt");

/*
 * Part one
 */

var partOneResult = (await reader.ReadLineByLine()
    .Select(line => line.Split(" | ")[1])
    .Select(outputPart => outputPart.Split(' '))
    .ToArrayAsync())
    .SelectMany(line => line)
    .Count(output => output.Length is 2 or 3 or 4 or 7);

Console.WriteLine($"In the output values digits 1, 4, 7, 8 appear {partOneResult} times.");

/*
 * Part two
 */

var sumOfAllOutputValues = await reader.ReadLineByLine()
    .Select(line => line.Split(" | "))
    .Select(
        parts => new
        {
            Input = parts[0].Split(' ').Select(i => i.ToDisplaySegments()).ToArray(), 
            Display = parts[1].Split(' ').Select(i => i.ToDisplaySegments()).ToArray()
        })
    .Select(
        lines => new
        {
            DecodedInput = new DamagedSignalDecoder().Decode(lines.Input), 
            lines.Display
        })
    .Select(lines => lines.Display.Select(digit => lines.DecodedInput[digit]).ToArray())
    .Select(digits => new DigitsToDecimalConverter().Convert(digits))
    .SumAsync();

Console.WriteLine($"The sum of all output values is {sumOfAllOutputValues}.");