namespace AdventOfCode.Day02.Commands;

internal class ForwardCommand : 
    ISubmarineCommand<SubmarineState>, 
    ISubmarineCommand<SubmarineStateWithAim>
{
    public SubmarineState Do(SubmarineState state, int arg) =>
        state with { HorizontalPosition = state.HorizontalPosition + arg };

    public SubmarineStateWithAim Do(SubmarineStateWithAim state, int arg) =>
        state with 
        { 
            HorizontalPosition = state.HorizontalPosition + arg, 
            Depth = state.Depth + (state.Aim * arg) 
        };
}