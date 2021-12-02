namespace AdventOfCode.Day02.Commands;

internal class UpCommand : 
    ISubmarineCommand<SubmarineState>, 
    ISubmarineCommand<SubmarineStateWithAim>
{
    public SubmarineState Do(SubmarineState state, int arg) =>
        state with { Depth = state.Depth - arg };

    public SubmarineStateWithAim Do(SubmarineStateWithAim state, int arg) =>
        state with { Aim = state.Aim - arg };
}