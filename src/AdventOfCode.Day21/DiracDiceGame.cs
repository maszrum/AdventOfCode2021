namespace AdventOfCode.Day21;

internal class DiracDiceGame
{
    private const int PointsToWin = 1000;
    
    private readonly DeterministicDice _dice;
    private readonly PlayerState _playerOneState;
    private readonly PlayerState _playerTwoState;
    
    private bool _playerOneTurn = true;
    
    public DiracDiceGame(DeterministicDice dice, int playerOneStartingPosition, int playerTwoStartingPosition)
    {
        _dice = dice;
        
        _playerOneState = new PlayerState(playerOneStartingPosition);
        _playerTwoState = new PlayerState(playerTwoStartingPosition);
    }

    public int PlayerOnePoints => _playerOneState.Points;
    
    public int PlayerOnePosition => _playerOneState.Position;
    
    public int PlayerTwoPoints => _playerTwoState.Points;
    
    public int PlayerTwoPosition => _playerTwoState.Position;
    
    public bool IsEnded => PlayerOnePoints >= PointsToWin || PlayerTwoPoints >= PointsToWin;
    
    public void Turn()
    {
        var player = _playerOneTurn 
            ? _playerOneState 
            : _playerTwoState;
        
        _playerOneTurn = !_playerOneTurn;
        
        var rollValues = Enumerable
            .Repeat(0, 3)
            .Select(_ => _dice.Roll());
        
        var sum = rollValues.Sum();
        
        player.MoveForward(sum);
    }
    
    public PlayerState GetLooser()
    {
        if (!IsEnded)
        {
            throw new InvalidOperationException(
                "game is has not been ended");
        }
        
        return PlayerOnePoints >= 1000 
            ? _playerTwoState 
            : _playerOneState;
    }
}