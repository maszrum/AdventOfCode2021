namespace AdventOfCode.Day02;

internal interface ISubmarineCommand<T>
{
    T Do(T state, int arg);
}