var reader = new FileLineByLineReader("input.txt");

var result = reader
    .CountAsync();

Console.WriteLine(result);