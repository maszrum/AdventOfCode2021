namespace AdventOfCode.Day08;

internal class DamagedSignalDecoder
{
    public Dictionary<DisplaySegments, int> Decode(
        IReadOnlyList<DisplaySegments> corruptedInput)
    {
        var one = corruptedInput.First(s => s.CountSegments() == 2);
        var four = corruptedInput.First(s => s.CountSegments() == 4);
        var seven = corruptedInput.First(s => s.CountSegments() == 3);
        var eight = corruptedInput.First(s => s.CountSegments() == 7);
        
        var segmentsOfLengthFive = corruptedInput
            .Where(s => s.CountSegments() == 5)
            .ToArray();
        
        var three = segmentsOfLengthFive.First(s => (s | one) == s);
        var two = segmentsOfLengthFive.First(s => (s | four) == eight);
        var five = segmentsOfLengthFive.First(s => s != three && s != two);
        
        var segmentsOfLengthSix = corruptedInput
            .Where(s => s.CountSegments() == 6)
            .ToArray();
        
        var six = segmentsOfLengthSix.First(s => (s & one).CountSegments() == 1);
        var nine = segmentsOfLengthSix.First(s => (s | four) == s);
        var zero = segmentsOfLengthSix.First(s => s != six && s != nine);
        
        return new Dictionary<DisplaySegments, int>
        {
            [zero] = 0,
            [one] = 1,
            [two] = 2,
            [three] = 3,
            [four] = 4,
            [five] = 5,
            [six] = 6,
            [seven] = 7,
            [eight] = 8,
            [nine] = 9
        };
    }
}