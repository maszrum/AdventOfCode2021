namespace AdventOfCode.Day19;

internal record MatchedScanners(Scanner ScannerA, Scanner ScannerB)
{
    public static MatchedScanners Reverse(MatchedScanners input) => 
        new(input.ScannerB, input.ScannerA);
}