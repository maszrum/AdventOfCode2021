var reader = new InputFileReader("input.txt");

var linesGreater = await reader
    .ReadLineByLine()
    .Select(int.Parse)
    .SelectWithPrevious((previous, current) => current > previous)
    .CountAsync(isGreater => isGreater);

Console.WriteLine($"There are {linesGreater} measurements larger than the previous measurement.");

var lineSumsGreater = await reader
    .ReadLineByLine()
    .Select(int.Parse)
    .SelectWithPrevious((previousPrevious, previous, current) => previousPrevious + previous + current)
    .SelectWithPrevious((previous, current) => current > previous)
    .CountAsync(isGreater => isGreater);

Console.WriteLine($"There are {lineSumsGreater} sums larger than the previous sum.");