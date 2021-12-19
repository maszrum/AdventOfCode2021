namespace AdventOfCode.Day18.Operations;

internal class SearchOperation
{
    public SearchResult FindLeftValue(SnailfishNumber number)
    {
        var current = number;
        
        while (!current.IsRoot)
        {
            var previous = current;
            current = current.Parent;
            
            if (current.Left.IsValue)
            {
                break;
            }
            
            if (current.Left.Pair == previous && current.IsRoot)
            {
                return SearchResult.NotFound();
            }
            
            if (current.Left.Pair != previous)
            {
                break;
            }
        }
        
        if (current.Left.IsValue)
        {
            return SearchResult.FoundInLeft(current);
        }
        
        current = current.Left.Pair;
        
        while (current.Right.IsPair)
        {
            current = current.Right.Pair;
        }
        
        return SearchResult.FoundInRight(current);
    }

    public SearchResult FindRightValue(SnailfishNumber number)
    {
        var current = number;
        
        while (!current.IsRoot)
        {
            var previous = current;
            current = current.Parent;
            
            if (current.Right.IsValue)
            {
                break;
            }
            
            if (current.Right.Pair == previous && current.IsRoot)
            {
                return SearchResult.NotFound();
            }
            
            if (current.Right.Pair != previous)
            {
                break;
            }
        }
        
        if (current.Right.IsValue)
        {
            return SearchResult.FoundInRight(current);
        }
        
        current = current.Right.Pair;
        
        while (current.Left.IsPair)
        {
            current = current.Left.Pair;
        }
        
        return SearchResult.FoundInLeft(current);
    }
}