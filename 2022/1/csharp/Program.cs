var lines = System.IO.File.ReadAllLines("./input.txt");
var elves = new List<int>();

var calories = new List<int>();
var current = 0;

foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        calories.Add(current);
        current = 0;

        continue;
    }

    current += int.Parse(line);
}

var topThree = calories.OrderByDescending(i => i).Take(3);

// Part One
Console.WriteLine(topThree.Max());

// Part Two
Console.WriteLine(topThree.Sum());