using System.Text.RegularExpressions;

var lines = File.ReadAllLines("./input.txt").ToList();
var endOfSetup = lines.FindIndex(l => string.IsNullOrWhiteSpace(l));
var stacks = LoadStacks(lines);

var instructionLines = lines.Skip(endOfSetup + 1);
var instructions = ParseInstructions(instructionLines);

// Part One
instructions
    .ForEach(i => ExecuteInstruction(stacks, i));

var topOfStacks = string.Join("", stacks.Select(s => s.Peek()));
System.Console.WriteLine(topOfStacks);

static void ExecuteInstruction(List<Stack<char>> stacks, (int, int, int) instruction)
{
    for (int move = 0; move < instruction.Item1; move++)
    {
        var popped = stacks[instruction.Item2].Pop();
        stacks[instruction.Item3].Push(popped);
    }
}

// Part Two
var stacksPartTwo = LoadStacks(lines);

instructions
    .ForEach(i => ExecuteInstructionPartTwo(stacksPartTwo, i));

var topOfStacksPartTwo = string.Join("", stacksPartTwo.Select(s => s.Peek()));
System.Console.WriteLine(topOfStacksPartTwo);

// Helpers
static void ExecuteInstructionPartTwo(List<Stack<char>> stacks, (int, int, int) instruction)
{
    var s = new Stack<char>();

    for (int move = 0; move < instruction.Item1; move++)
    {
        var popped = stacks[instruction.Item2].Pop();
        s.Push(popped);
    }

    while (s.TryPop(out var item)) {
        stacks[instruction.Item3].Push(item);
    }
}

static List<(int, int, int)> ParseInstructions(IEnumerable<string> lines) =>
    lines
        .Select(line =>
        {
            var pattern = @"move (\d+) from (\d+) to (\d+)";
            var match = Regex.Match(line, pattern);

            return ParseMatch(match);
        })
        .ToList();

static (int, int, int) ParseMatch(Match match) =>
    (ParseGroup(match.Groups[1]), ParseGroup(match.Groups[2]) - 1, ParseGroup(match.Groups[3]) - 1);

static int ParseGroup(Group group) => int.Parse(group.ToString());

static List<Stack<char>> LoadStacks(List<string> lines)
{
    var endOfSetup = lines.FindIndex(l => string.IsNullOrWhiteSpace(l));

    var stackCountLine = lines[endOfSetup - 1].Trim();
    var stackCount = stackCountLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).Max();
    var stacks = Enumerable.Range(0, stackCount).Select(_ => new Stack<char>()).ToList();

    var setupLines = lines.Take(endOfSetup - 1).ToList();
    var setupLineMaxIndex = setupLines.Count - 1;

    for (int i = setupLineMaxIndex; i >= 0; i--)
    {
        var line = setupLines[i];
        var index = 1;

        while (index <= line.Length)
        {
            var letter = line[index];

            if (!char.IsWhiteSpace(letter))
            {
                stacks[(index - 1) / 4].Push(letter);
            };

            index += 4;
        }
    }

    return stacks;
}