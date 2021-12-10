using System.Text;

namespace AdventOfCode.Day10;

internal class BracketSyntaxChecker
{
    private readonly string _input;
    private readonly Stack<char> _bracketsStack = new();

    private int _currentCharacterIndex;

    public BracketSyntaxChecker(string input)
    {
        _input = input;
    }
    
    public bool TryGetNextError(out int characterIndex)
    {
        while (_currentCharacterIndex < _input.Length)
        {
            var character = _input[_currentCharacterIndex];
            _currentCharacterIndex++;
            
            var isOpening = character.IsOpeningBracket();
            var isClosing = character.IsClosingBracket();
            
            if (!isOpening && !isClosing)
            {
                continue;
            }
            
            if (isOpening)
            {
                _bracketsStack.Push(character);
            }
            else
            {
                var lastOnStack = _bracketsStack.Pop();
                
                if (character.ToOpeningBracket() != lastOnStack)
                {
                    characterIndex = _currentCharacterIndex - 1;
                    return true;
                }
            }
        }
        
        characterIndex = default;
        return false;
    }

    public string GetMissingPart()
    {
        if (_currentCharacterIndex < _input.Length)
        {
            throw new InvalidOperationException(
                $"call {nameof(TryGetNextError)} until it returns false");
        }
        
        var sb = new StringBuilder();
        
        while (_bracketsStack.TryPop(out var openingBracket))
        {
            var closingBracket = openingBracket.ToClosingBracket();
            sb.Append(closingBracket);
        }
        
        return sb.ToString();
    }
}