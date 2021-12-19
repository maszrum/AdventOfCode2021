namespace AdventOfCode.Day18.Operations;

internal class ExplodeOperation
{
    private readonly SearchOperation _searchOperation = new();
    
    public bool Do(SnailfishNumber number) => 
        Explode(number, 0);

    private bool Explode(SnailfishNumber number, int depth)
    {
        if (depth < 4)
        {
            if (number.Left.IsPair)
            {
                var exploded = Explode(number.Left.Pair, depth + 1);
                
                if (exploded)
                {
                    return true;
                }
            }
            if (number.Right.IsPair)
            {
                var exploded = Explode(number.Right.Pair, depth + 1);
                
                if (exploded)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        var leftValue = number.Left.Value;
        var leftSearchResult = _searchOperation.FindLeftValue(number);
        
        if (leftSearchResult.Found && leftSearchResult.InLeft)
        {
            leftSearchResult.Number.Left = SnailfishValue.CreateValue(
                leftSearchResult.Number.Left.Value + leftValue);
        }
        else if (leftSearchResult.Found && leftSearchResult.InRight)
        {
            leftSearchResult.Number.Right = SnailfishValue.CreateValue(
                leftSearchResult.Number.Right.Value + leftValue);
        }
        
        var rightValue = number.Right.Value;
        var rightSearchResult = _searchOperation.FindRightValue(number);
        
        if (rightSearchResult.Found && rightSearchResult.InLeft)
        {
            rightSearchResult.Number.Left = SnailfishValue.CreateValue(
                rightSearchResult.Number.Left.Value + rightValue);
        }
        else if (rightSearchResult.Found && rightSearchResult.InRight)
        {
            rightSearchResult.Number.Right = SnailfishValue.CreateValue(
                rightSearchResult.Number.Right.Value + rightValue);
        }
        
        var parent = number.Parent;
        
        if (parent.Left.IsPair && parent.Left.Pair == number)
        {
            parent.Left = SnailfishValue.CreateValue(0);
            number.RemoveParent();
        }
        else if (parent.Right.IsPair && parent.Right.Pair == number)
        {
            parent.Right = SnailfishValue.CreateValue(0);
            number.RemoveParent();
        }
        
        return true;
    }
}