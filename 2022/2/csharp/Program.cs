var lines = System.IO.File.ReadAllLines("./input.txt");

// Part 1
var score = GetTotalScore(lines);

Console.WriteLine(score);

// Part 2
var translated = lines.Select(TranslateLine);
var newScore = GetTotalScore(translated);

Console.WriteLine(newScore);

// Helpers
static int GetTotalScore(IEnumerable<string> lines) => lines.Aggregate(0, (acc, line) => acc += GetScore(line));

static int GetScore(string line) => line switch
{
    "A X" => 4, // rock     rock -> 3 + 1 = 4
    "B X" => 1, // paper    rock -> 0 + 1 = 1
    "C X" => 7, // scissors rock -> 6  + 1 = 7
    "A Y" => 8, // rock     paper -> 6 + 2 = 8
    "B Y" => 5, // paper    paper -> 3 + 2 = 5
    "C Y" => 2, // scissors paper -> loss + 2 = 2
    "A Z" => 3, // rock     scissors -> loss + 3 = 3
    "B Z" => 9, // paper    scissors -> win + 3 = 9
    "C Z" => 6, // scissors scissors -> draw + 3 = 6
    _ => 0
};

static string TranslateLine(string line)
{
    var opposition = line.First().ToString();
    var outcome = line.Last().ToString();

    if (outcome == "X") // Lose
    {
        outcome = opposition switch
        {
            "A" => "Z",
            "B" => "X",
            _ => "Y"
        };
    }
    else if (outcome == "Y") // Draw
    {
        outcome = opposition switch
        {
            "A" => "X",
            "B" => "Y",
            _ => "Z"
        };
    }
    else // Win
    {
        outcome = opposition switch
        {
            "A" => "Y",
            "B" => "Z",
            _ => "X"
        };
    }

    return $"{opposition} {outcome}";
}