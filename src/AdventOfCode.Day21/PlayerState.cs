namespace AdventOfCode.Day21;

internal class PlayerState
{
    public PlayerState(int position)
    {
        Position = position;
    }

    public int Points { get; private set; }
    
    public int Position { get; private set; }
    
    public void MoveForward(int times)
    {
        Position += times;
        
        while (Position > 10)
        {
            Position -= 10;
        }
        
        Points += Position;
    }
}