var lines = File.ReadAllLines("./input.txt");

// Part One
var sum = lines
    .Select(l =>
    {
        var half = l.Length / 2;
        var firstHalf = l.Substring(0, half).ToHashSet();
        var secondHalf = l.Substring(half).ToHashSet();
        var intersect = firstHalf.Intersect(secondHalf).Single();
        return GetPriority(intersect);
    })
    .Sum();

Console.WriteLine(sum);

// Part Two
var sumTwo = lines
    .Select(l => l.ToHashSet())
    .Chunk(3)
    .Select(group => {
        var intersectOne = group[0].Intersect(group[1]);
        var intersectTwo = intersectOne.Intersect(group[2]);

        return GetPriority(intersectTwo.Single());
    })
    .Sum();

Console.WriteLine(sumTwo);

static int GetPriority(char i)
{
    var num = (int)i % 32;
    return Char.IsUpper(i) ? num + 26 : num;
}