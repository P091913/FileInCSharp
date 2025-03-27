using static System.Console;

var factArray = new string[10];

var value = 45.34;

WriteLine(value.ToString("C2"));
    

Init();
if (factArray.GetLength(0) < 1)
{
    WriteLine("File not found please put file into `Debug/netX.X/facts.dat`");
    //https://learn.microsoft.com/en-us/windows/win32/debug/system-error-codes--0-499-?redirectedfrom=MSDN

    Environment.Exit(2);
}

bool again;
do
{
    //WriteLine(value.ToString("C2"));
    var factPicked = InputNumber();
    DisplayFact(factPicked);
    again = AskForAnother();
} while (again);

DisplayAllFacts();
return;


bool AskForAnother()
{
    WriteLine("\nWould you like another fact about #C?");
    do
    {
        WriteLine("Enter 'Yes' or 'No'");
        var userInput = ReadLine();
        if (string.IsNullOrWhiteSpace(userInput)) continue;
        if (userInput.Equals("Yes", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        WriteLine("Goodbye");
        WriteLine("Press Enter to Close");
        ReadLine();
        return false;
    }while (true) ;
}

void DisplayFact(int factPicked)
{
    Write("Fact: ");
    WriteLine(factArray[factPicked - 1]);
}

int InputNumber()
{
    var inputNumber = 0;
    bool valid;
    do
    {
        Write("To display a fact, Enter 1 - 10: ");
        try
        {
            inputNumber = Convert.ToInt32(ReadLine());
            if (inputNumber is > 0 and < 10)
            {
                valid = true;
            }
            else
            {
                WriteLine("Error. Input number must be the numbers 1-10.");
                WriteLine("Press enter to try again");
                ReadLine();
                valid = false;
            }
        }
        catch (Exception)
        {
            WriteLine("Error! Your number MUST BE NUMERIC.");
            WriteLine("Press Enter to try again");
            ReadLine();
            valid = false;
        }
    } while (!valid);

    return inputNumber;
}


void Init()
{
    WriteLine("Welcome this console app displays a fact based on user input number");
    // @ sign is a string literal, tells the compiler to treat the string exactly as written,
    // \ are not seen as escape characters
    const string filename = @"./facts.dat";

    //ASK: do we want to crash the program or not?
    try
    {
        // try
        // {
        //     factArray = File.ReadAllLines(fileName).ToArray();
        // }
        // catch (FileNotFoundException)
        // {
        //     Error.WriteLine("File not found look at bin/debug/net.x.x/");
        // }
        
        var inputFile = new FileStream(filename, FileMode.Open, FileAccess.Read);
        var reader = new StreamReader(inputFile);
        var recordIn = reader.ReadLine();

        for (var i = 0; recordIn != null && i < 10; i++)
        {
            factArray[i] = recordIn;
            recordIn = reader.ReadLine();
        }
        
        DisplayAllFacts();

        reader.Close();
        inputFile.Close();
    }
    catch (FileNotFoundException ex)
    {
        Error.WriteLine(ex);
    }
}

void DisplayAllFacts()
{
    // clears our console
    Clear();

    //how to add spacing
    //https://docs.microsoft.com/en-us/dotnet/standard/base-types/composite-formatting#alignment-component/
    WriteLine("{0,-8}{1,-40}", "Number", "Fact");
    WriteLine("----------------------------------------------------------------------");

    for (var i = 0; i < factArray.GetLength(0); i++)
    {
        // number references the arguments on the right side
        // number followed by left aligning values within a field of 8 charactes in length
        // left aligning value within a field of 50 characters in length
        WriteLine("{0,-8}{1,-50}", i, factArray[i]);
    }
}