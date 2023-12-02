Console.WriteLine("Hello, World!");

var lines = File.ReadLines("./input.txt");

var gameableCount = 0;
var colors = new Dictionary<string, int>
{
    { "red", 12 },
    { "green", 13 },
    { "blue", 14 }
};

foreach (var line in lines)
{
    var validGame = true;
    var pulls = line.Split(';');
    var colonIndex = pulls[0].IndexOf(':');
    var gameId = int.Parse(pulls[0].Substring(5, colonIndex - 5));
    
    pulls[0] = pulls[0].Substring(colonIndex + 2);
    foreach (var pull in pulls)
    {
        var colorCounts = pull.Trim().Split(", ");
        foreach (var colorCount in colorCounts)
        {
            var countAndColor = colorCount.Split(' ');
            var maxValue = colors[countAndColor[1]];
            var numberOfColorPulled = int.Parse(countAndColor[0]);
            if (numberOfColorPulled > maxValue)
            {
                validGame = false;
            }
        }
    }

    if (validGame)
    {
        gameableCount += gameId;
    }
    
    // Console.WriteLine($"GameId: {gameId}, Gameable: {validGame}");
}

// Console.WriteLine(gameableCount);

//////////////////
Console.WriteLine("Part 2");

var powers = 0;
foreach (var line in lines)
{
    var validGame = true;
    var pulls = line.Split(';');
    var colonIndex = pulls[0].IndexOf(':');
    pulls[0] = pulls[0].Substring(colonIndex + 2);

    var maxCounts = new Dictionary<string, int>();
    maxCounts.Add("red", 0);
    maxCounts.Add("green", 0);
    maxCounts.Add("blue", 0);
    
    foreach (var pull in pulls)
    {
        var colorCounts = pull.Trim().Split(", ");
        foreach (var colorCount in colorCounts)
        {
            var countAndColor = colorCount.Split(' ');
            var cubesNeeded = int.Parse(countAndColor[0]);
            var minNumberOfCubedNeeded = maxCounts[countAndColor[1]];
            if (minNumberOfCubedNeeded < cubesNeeded)
            {
                maxCounts[countAndColor[1]] = cubesNeeded;
            }
        }
    }
    
    Console.WriteLine($"Minimums: Red {{{maxCounts["red"]}}}, Green {{{maxCounts["green"]}}}, Blue {{{maxCounts["blue"]}}}");
    var power = maxCounts["red"] * maxCounts["green"] * maxCounts["blue"];
    powers += power;
    Console.WriteLine($"Power: {power}");
}

Console.WriteLine(powers);

