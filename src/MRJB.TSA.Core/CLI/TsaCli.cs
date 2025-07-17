using MRJB.TSA.Core.Models;

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
        ShowLogo();
        WriteYellow("https://github.com/mrjamiebowman/tsa");
        WriteYellow("https://www.mrjamiebowman.com/tsa");
        Console.WriteLine("");
    }

    public static void GenerateJoke()
    {
        WriteGreen(@"");
        WriteGreen($"👮 TSA Agent: {GenerateRandomScreeningReportMessage()}");
        WriteGreen(@"");
    }

    public static void ShowHelp()
    {
        Console.WriteLine("✈️ Terrible Settings Auditor (TSA)");
        Console.WriteLine("tsa --help                Show help info");
        Console.WriteLine("tsa --validate            Ensures all configuration and settings are valid.");
        Console.WriteLine("tsa --generate-config     Creates a scaffolded tsa.json configuration file for TSA settings.");
        Console.WriteLine("tsa --joke                On the house — courtesy of your flight.");
    }

    public static string CenterText(string text, int width)
    {
        text = text.Trim();

        if (text.Length >= width - 2) {
            text = text.Substring(0, width - 4) + "..";
        }

        int padding = (width - text.Length) / 2;
        return new string(' ', padding) + text;
    }

    public static void ShowBlock(string title, string? message = null, int x = 60)
    {
        var strBlock  = new string('*', x);

        WriteGreen(strBlock);
        WriteGreen(CenterText(title, x));
        WriteGreen(strBlock);

        if (!String.IsNullOrWhiteSpace(message))
        {
            Console.WriteLine("");
            WriteGreen(message);
            Console.WriteLine("");
        }
    }

    public static void ShowReport(ScreeningReport screeningReport)
    {
        WriteGreen($"👮 TSA Agent: {GenerateRandomScreeningReportMessage()}");
        Console.WriteLine("");
        ShowBlock("📄 Screening Report");

        // summary

        // configs
    }

    public static string GenerateRandomScreeningReportMessage()
    {
        string[] tsaLines = new[]
        {
            "Sir, your bag just made a noise we’ve only heard in spy movies. Mind stepping over here before it takes off on its own?",
            "We’re gonna need you to step to the side. Your bag has more white powder than Tony Montana’s desk.",
            "Sir, we saw you sweating like you're smuggling fireworks on the Fourth of July. Quick chat over here?",
            "You’ve been randomly selected by the Wheel of Misfortune™. Please step aside. Yes, again.",
            "Ma’am, you’re too calm for a 6 a.m. flight. Step over here. We need to know your secrets. And maybe your skincare routine.",
            "Sir, you’re wearing Gucci slides, a Rolex, and 17 gold chains… on Spirit Airlines. Just a quick check to make sure you’re not the plane's new owner.",
            "Sir, you said 'bomb' seven times while arguing with your mom on FaceTime. Just gonna go ahead and need you to follow us over here. Gently.",
            "Sir, I get it — barefoot is freeing. But this is an airport, not a yoga retreat. Let’s talk about hygiene over here."
        };

        Random rnd = new Random();
        int index = rnd.Next(tsaLines.Length);
        string selectedLine = tsaLines[index];

        return selectedLine;
    }
}
