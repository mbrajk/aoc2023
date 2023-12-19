// import row by row, read character by character into a matrix
// The matrix data is a x for no meaningful data, a n for a number
// and a s for a symbol

//e.g. 
// xxnnnxx
// xxxxsxx
// xnnxxxx

// make a second pass, we convert the ns which are touching a part to T
// s and n which are not touching a part are ignored for now

// xxnTTxx
// xxxxsxx
// xnnxxxx

//final pass we find all n which are touching a T and convert them to T
// xxTTTxx
// xxxxsxx
// xnnxxxx

// last pass, all T are parts straight lookup from original data, add to list, iterate list and add
// this is overall a crappy solution

var lines = File.ReadLines("./input.txt");

var lineMatrix = lines.Select(l => l.ToArray()).ToArray();

var convertedMatrix = new char[lines.Count()][];
for (int mm = 0; mm < lines.Count(); mm++)
{
    convertedMatrix[mm] = new char[lines.First().Count()];
}

for (int i = 0; i < lines.Count(); i++)
{
    var line = lineMatrix[i];
    for (int j = 0; j < lines.First().Count(); j++)
    {
        char toUse;
        if (line[j] == '.')
        {
            toUse = 'x';
        }
        else if (char.IsDigit(line[j]))
        {
            toUse = 'n';
        }
        else
        {
            toUse = 's';
        }

        convertedMatrix[i][j] = toUse;
    }
}

for (int i = 0; i < lines.Count(); i++)
{
    var prevLine = i > 0 ? convertedMatrix[i - 1] : null;
    var line = convertedMatrix[i];
    var nextLine = i + 1 < lines.Count() ? convertedMatrix[i + 1] : null;

    for (int j = 0; j < lines.First().Count(); j++)
    {
        if (convertedMatrix[i][j] != 'n') continue;
        if (prevLine != null)
        {
            if (prevLine[j] == 's')
            {
                convertedMatrix[i][j] = 'T';
            }
            else if (j > 0 && prevLine[j - 1] == 's')
            {
                convertedMatrix[i][j] = 'T';
            }
            else if (j + 1 < prevLine.Length && prevLine[j + 1] == 's')
            {
                convertedMatrix[i][j] = 'T';
            }
        }

        if (nextLine != null)
        {
            if (nextLine[j] == 's')
            {
                convertedMatrix[i][j] = 'T';
            }
            else if (j > 0 && nextLine[j - 1] == 's')
            {
                convertedMatrix[i][j] = 'T';
            }
            else if (j + 1 < nextLine.Length && nextLine[j + 1] == 's')
            {
                convertedMatrix[i][j] = 'T';
            }
        }

        if (j > 0 && line[j - 1] == 's')
        {
            convertedMatrix[i][j] = 'T';
        }

        if (j + 1 < line.Length && line[j + 1] == 's')
        {
            convertedMatrix[i][j] = 'T';
        }
    }
}

var numberStringos = new List<int>();

for (int i = 0; i < lines.Count(); i++)
{
    for (int j = 0; j < lines.First().Count(); j++)
    {
        if (convertedMatrix[i][j] != 'T') continue;

        string numberStringo = lineMatrix[i][j].ToString();

        int baxo = j;
        int forwardzo = j;
        while (baxo - 1 >= 0)
        {
            if (convertedMatrix[i][baxo - 1] == 'n' || convertedMatrix[i][baxo - 1] == 'T')
            {
                convertedMatrix[i][baxo - 1] = 'T';
                numberStringo = lineMatrix[i][baxo - 1] + numberStringo;
                baxo -= 1;
            }
            else break;
        }

        while (forwardzo < lines.First().Count() - 1)
        {
            if (convertedMatrix[i][forwardzo + 1] == 'n' || convertedMatrix[i][forwardzo + 1] == 'T')
            {
                convertedMatrix[i][forwardzo + 1] = 'T';
                numberStringo = numberStringo + lineMatrix[i][forwardzo + 1];
                forwardzo += 1;
            }
            else break;
        }


        j = forwardzo + 1;
        numberStringos.Add(int.Parse(numberStringo));
    }
}

Console.WriteLine(convertedMatrix);
foreach (var numberStringo in numberStringos)
{
    Console.WriteLine(numberStringo);
}

Console.WriteLine(numberStringos.Sum());