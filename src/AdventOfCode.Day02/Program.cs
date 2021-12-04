var reader = new InputFileReader("input.txt");

/*
 *  Part one
 */

var firstCommandsFactory = new Dictionary<string, Func<ISubmarineCommand<SubmarineState>>>
{
    ["forward"] = () => new ForwardCommand(),
    ["down"] = () => new DownCommand(),
    ["up"] = () => new UpCommand()
};

var firstFinalState = await reader
    .ReadLineByLine()
    .Select(line => line.Split(' '))
    .Select(array => (Command: array[0], Arg: int.Parse(array[1])))
    .AggregateAsync(new SubmarineState(0, 0), (currentState, step) =>
    {
        var command = firstCommandsFactory[step.Command]();
        return command.Do(currentState, step.Arg);
    });

var firstResult = firstFinalState.HorizontalPosition * firstFinalState.Depth;

Console.WriteLine(firstFinalState);
Console.WriteLine($"Multiplication of final horizontal position by final depth is equal to {firstResult}");

/*
 *  Part two
 */

var secondCommandsFactory = new Dictionary<string, Func<ISubmarineCommand<SubmarineStateWithAim>>>
{
    ["forward"] = () => new ForwardCommand(),
    ["down"] = () => new DownCommand(),
    ["up"] = () => new UpCommand()
};

var secondFinalState = await reader
    .ReadLineByLine()
    .Select(line => line.Split(' '))
    .Select(array => (Command: array[0], Arg: int.Parse(array[1])))
    .AggregateAsync(new SubmarineStateWithAim(0, 0, 0), (currentState, step) =>
    {
        var command = secondCommandsFactory[step.Command]();
        return command.Do(currentState, step.Arg);
    });

var secondResult = secondFinalState.HorizontalPosition * secondFinalState.Depth;

Console.WriteLine("");
Console.WriteLine(secondFinalState);
Console.WriteLine($"Multiplication of final horizontal position by final depth is equal to {secondResult}");