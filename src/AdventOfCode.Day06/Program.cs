var reader = new InputFileReader("input.txt");

var input = (await reader.ReadAllLines())[0];

var lanternFish = input
    .Split(',')
    .Select(int.Parse)
    .GroupBy(
        internalTimer => internalTimer, 
        (internalTimer, fish) => (InternalTimer: internalTimer, Count: fish.LongCount()))
    .ToDictionary(g => g.InternalTimer, g => g.Count);

var school = new SchoolOfLanternFish(lanternFish);

for (var day = 1; day <= 80; day++)
{
    school.NextDay();
}

Console.WriteLine($"After 80 days, there would be {school.Count()} lantern fish.");

for (var day = 81; day <= 256; day++)
{
    school.NextDay();
}

Console.WriteLine($"After 256 days, there would be {school.Count()} lantern fish.");