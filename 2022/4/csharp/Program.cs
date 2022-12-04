var lines = File.ReadAllLines("input.txt");

var parsedRanges = lines
    .Select(l => l.Split(','))
    .Select(l => (ParseRange(l[0]), ParseRange(l[1])));

// Part One
var completeOverlaps =
    parsedRanges
        .Where(a => CompletelyOverlaps(a.Item1, a.Item2) || CompletelyOverlaps(a.Item2, a.Item1));

Console.WriteLine(completeOverlaps.Count());

// Part Two
var partialOverlaps =
    parsedRanges
        .Where(a => PartiallyOverlaps(a.Item1, a.Item2) || PartiallyOverlaps(a.Item2, a.Item1));

Console.WriteLine(partialOverlaps.Count());

static (int, int) ParseRange(string line)
{
    var hyphen = line.IndexOf("-");
    var start = int.Parse(line.Substring(0, hyphen));
    var end = int.Parse(line.Substring(hyphen + 1));

    return (start, end);
}

static bool CompletelyOverlaps((int, int) rangeOne, (int, int) rangeTwo)
    => rangeOne.Item1 >= rangeTwo.Item1 && rangeOne.Item2 <= rangeTwo.Item2;

static bool PartiallyOverlaps((int, int) rangeOne, (int, int) rangeTwo)
    => (rangeOne.Item1 >= rangeTwo.Item1 && rangeOne.Item1 <= rangeTwo.Item2) || (rangeOne.Item2 >= rangeTwo.Item1 && rangeOne.Item2 <= rangeTwo.Item2);