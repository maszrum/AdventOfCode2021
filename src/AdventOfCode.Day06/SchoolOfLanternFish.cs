namespace AdventOfCode.Day06;

internal class SchoolOfLanternFish
{
    private readonly Dictionary<int, long> _count;
    
    public SchoolOfLanternFish(Dictionary<int, long> count)
    {
        _count = count.ToDictionary(k => k.Key, v => v.Value);
        
        for (var i = 0; i <= 8; i++)
        {
            if (!_count.ContainsKey(i))
            {
                _count.Add(i, 0);
            }
        }
    }
    
    public void NextDay()
    {
        var zeroInternalTimerFish = _count[0];
        
        for (var i = 1; i <= 8; i++)
        {
            _count[i - 1] = _count[i];
        }
        _count[6] += zeroInternalTimerFish;
        _count[8] = zeroInternalTimerFish;
    }
    
    public long Count() => _count.Values.Sum();
}