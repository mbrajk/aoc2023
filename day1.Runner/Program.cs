Console.WriteLine("Hello, World!");

var lines = File.ReadLines("./input.txt");

var sum = 0;
var intLookup = new Dictionary<string, int>
{
    {"one", 1},
    {"two", 2},
    {"three", 3},
    {"four", 4},
    {"five", 5},
    {"six", 6},
    {"seven", 7},
    {"eight", 8},
    {"nine", 9}
};

// lines = new List<string>
// {
//     "wxoxthree", //33
//     "1", //11
//     "12", //12
//     "5two5", //55
//     "567857" //57
// };
var firstCharacters = "otfsen";

foreach (var line in lines)
{
    var firstNumber = -1;
    var lastNumber = -1;
    
    for (var i = 0; i < line.Length; i ++)
    {
        var start = i;
        var end = line.Length - (i + 1);
        
        // we found none or only 1 number so far and we aren't done searching the string
        if (firstNumber == -1)
        {
            //check if this is a number and if it is save the value
            var characterAtIndex = line[start];
            if (Char.IsDigit(characterAtIndex))
            {
                firstNumber = int.Parse(characterAtIndex.ToString());
            }
            else if (firstCharacters.Contains(characterAtIndex))
            {
                //we matched a first character so there's a possibility its a number
                foreach (var l in intLookup)
                {
                    if (line.Substring(start).StartsWith(l.Key))
                    {
                        firstNumber = l.Value;
                    }
                }
            }
        }
        
        if (lastNumber == -1)
        {
            var characterAtIndex = line[end];
            if (Char.IsDigit(characterAtIndex))
            {
                lastNumber = int.Parse(characterAtIndex.ToString());
            }
            else if (firstCharacters.Contains(characterAtIndex))
            {
                //we matched a first character so there's a possibility its a number
                foreach (var l in intLookup)
                {
                    if (line.Substring(end).StartsWith(l.Key))
                    {
                        lastNumber = l.Value;
                    }
                }
            }
        }
        
        // we found two numbers or we found one number which is the only number 
        if (firstNumber > -1 && lastNumber > -1)
        {
            break;
        }
    }

    if (firstNumber == -1 || lastNumber == -1)
    {
        Console.WriteLine("🚨🚨wee woo🚨🚨");
    }

    Console.WriteLine($"{line} | {firstNumber}  {lastNumber}");
    
    sum += firstNumber * 10;
    sum += lastNumber;
}

Console.WriteLine(sum);