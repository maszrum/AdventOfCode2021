var reader = new InputFileReader("input.txt");

/*
 * Read pairs count from input template.
 *   from string "KKOSPHC..."
 *   to dictionary: ["KK" => 1, "KO" => 1, ...]
 */

var inputTemplate = await reader.ReadFirstLine();

var pairsCount = inputTemplate
    .Aggregate(
        seed: new { Previous = ' ', Pairs = new List<string>() }, 
        func: (state, current) =>
        {
            if (state.Previous != ' ')
            {
                var pair = string.Concat(state.Previous, current);
                state.Pairs.Add(pair);
            }
            return new { Previous = current, Pairs = state.Pairs };
        })
    .Pairs
    .GroupBy(pair => pair)
    .ToImmutableDictionary(g => g.Key, g => g.LongCount());

/*
 * Read insertion rules.
 */

var insertionRules = (await reader.ReadLineByLine()
    .Skip(2)
    .Select(line => line.Split(" -> "))
    .Select(parts => 
        new
        {
            Pair = parts[0], 
            Insertion = parts[1][0]
        })
    .ToDictionaryAsync(
        b => b.Pair, 
        b => b.Insertion))
    .ToImmutableDictionary();

/*
 * Step method
 */

ImmutableDictionary<string, long> Step(
    ImmutableDictionary<string, long> counts, 
    ImmutableDictionary<string, char> rules)
{
    var mutableCounts = counts
        .Where(kvp => kvp.Value != 0)
        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    
    foreach (var (pair, count) in counts)
    {
        if (rules.TryGetValue(pair, out var insertion))
        {
            mutableCounts[pair] -= count;
            
            if (mutableCounts[pair] == 0)
            {
                mutableCounts.Remove(pair);
            }
            
            var newPairOne = string.Concat(pair[0], insertion);
            var newPairTwo = string.Concat(insertion, pair[1]);
            
            if (!mutableCounts.ContainsKey(newPairOne))
            {
                mutableCounts.Add(newPairOne, 0);
            }
            
            if (!mutableCounts.ContainsKey(newPairTwo))
            {
                mutableCounts.Add(newPairTwo, 0);
            }
            
            mutableCounts[newPairOne] += count;
            mutableCounts[newPairTwo] += count;
        }
    }
    
    return mutableCounts.ToImmutableDictionary();
}

/*
 * Count method
 */

ImmutableDictionary<char, long> CountPolymerElements(
    string originalTemplate, 
    ImmutableDictionary<string, long> counts)
{
    var result = new Dictionary<char, long>()
    {
        [originalTemplate[0]] = 1,
        [originalTemplate[^1]] = 1
    };

    foreach (var (pair, count) in counts)
    {
        if (!result.ContainsKey(pair[0]))
        {
            result.Add(pair[0], 0);
        }
    
        if (!result.ContainsKey(pair[1]))
        {
            result.Add(pair[1], 0);
        }
    
        result[pair[0]] += count;
        result[pair[1]] += count;
    }
    
    return result
        .Select(kvp => KeyValuePair.Create(kvp.Key, kvp.Value / 2))
        .ToImmutableDictionary();
}

/*
 * Part one
 */

var partOnePairsCount = Enumerable
    .Repeat(0, 10)
    .Aggregate(pairsCount, (counts, _) => Step(counts, insertionRules));

var partOneElementsCount = CountPolymerElements(inputTemplate, partOnePairsCount);

var minPartOne = partOneElementsCount.Min(kvp => kvp.Value);
var maxPartOne = partOneElementsCount.Max(kvp => kvp.Value);

Console.WriteLine($"First part result: {maxPartOne - minPartOne}");

/*
 * Part two
 */

var partTwoPairsCount = Enumerable
    .Repeat(0, 40)
    .Aggregate(pairsCount, (counts, _) => Step(counts, insertionRules));

var partTwoElementsCount = CountPolymerElements(inputTemplate, partTwoPairsCount);

var minPartTwo = partTwoElementsCount.Min(kvp => kvp.Value);
var maxPartTwo = partTwoElementsCount.Max(kvp => kvp.Value);

Console.WriteLine($"Second part result: {maxPartTwo - minPartTwo}");