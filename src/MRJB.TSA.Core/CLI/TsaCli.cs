namespace MRJB.TSA.Core.CLI;

public static class TsaCli
{
    public static void WriteGreen(string message)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColor;
    }

    public static void WriteYellow(string message)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColor;
    }

    public static void WriteError(string message)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColor;
    }

    public static void ShowLogo()
    {
        WriteGreen(@"");
        WriteGreen(@"              ______");
        WriteGreen(@"             _\  _~-\___");
        WriteGreen(@"    =  = == (____MRJB___D");
        WriteGreen(@"                \_____\___________________,-~~~~~~~`-.._");
        WriteGreen(@"                / o O o o o o O O o o o o o o O o       |\_");
        WriteGreen(@"                `~-.__        ___..----..                  )");
        WriteGreen(@"                      `---~~\___________ / ------------`````");
        WriteGreen(@"                      =  === (_________D");
        WriteGreen(@"");
    }

    public static void ShowBanner()
    {
        Console.WriteLine("");
        WriteGreen("✈️ Terrible Settings Auditor (TSA)");
        WriteGreen("By: @mrjamiebowman");
        Console.WriteLine("");
        ShowLogo();
        Console.WriteLine("");        
        WriteYellow("https://github.com/mrjamiebowman/tsa");
        WriteYellow("https://www.mrjamiebowman.com/tsa");
        Console.WriteLine("");
        Console.WriteLine("");
    }

    public static void ShowHelp()
    {
        Console.WriteLine("✈️ Terrible Settings Auditor (TSA):");
        Console.WriteLine("  greet --name <YourName>   Greets the user with green text");
        Console.WriteLine("  --help                    Show help info");
    }
}
