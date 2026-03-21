using TerribleSettingsAuditor.Core.Models;

namespace TerribleSettingsAuditor.Core.CLI;

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
        WriteGreen(@"");
        WriteGreen("Terrible Settings Auditor is an independent developer tool and is not affiliated with or endorsed by the Transportation Security Administration.");
        ShowLogo();
        WriteYellow("https://github.com/mrjamiebowman/tsa");
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
        Console.WriteLine("tsa --screen              Ensures all configuration and settings are present.");
        //Console.WriteLine("tsa --generate          Creates a scaffolded tsa.json configuration file for TSA settings.");
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
        Console.WriteLine("");
        ShowBlock(" 📄 Screening Report");
        Console.WriteLine("");
        WriteGreen($" 👮 TSA Agent: {GenerateRandomScreeningReportMessage()}");
        Console.WriteLine("");

        // configs
        foreach (var item in screeningReport.Configuration)
        {
            WriteYellow($"{Icons.Luggage} Carry On: {item.Name}, ({item.Namespace}).");

            foreach (var prop in item.Properties)
            {
                //  ✅ Baggage Item: BlobStorage, Required: No
                var icon = prop.Pass ? Icons.Success : Icons.Failure;
                var required = (prop.Required ?? false) ? "Yes" : "No";

                WriteYellow($"- {icon} {prop.Name}, (Required: {required})" + prop.Message);
            }

            Console.WriteLine("");
        }
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
            "Sir, I get it — barefoot is freeing. But this is an airport, not a yoga retreat. Let’s talk about hygiene over here.",
            "Sir, your laptop has 47 open tabs, 3 mining scripts, and a crypto wallet. We're just gonna make sure it's not trying to fly itself.",
            "Ma’am, you whispered something about lizard people and 5G at Gate 12. We’re just gonna check your carry-on for tinfoil hats.",
            "Sir, you’ve been standing barefoot in security humming Jimmy Buffett songs for 30 minutes. Come with us before you start grilling.",
            "Sir, the entire terminal heard your group chat read-aloud. We just want to know how your cousin’s rash turned out.",
            "Ma’am, you’ve livestreamed every step of the TSA line. We just need a moment to sign the media release.",
            "Young man, we counted 42 questions about plane crashes in 6 minutes. Let's take a break from Google, okay?",
            "Sir, you loudly explained TSA procedure to 3 people and made up 12 rules. We’re just gonna ask you to stop helping.",
            "Ma’am, your purse contains a rotisserie chicken, 3 pickles, and a whole melon. We admire your commitment to in-flight dining. Let’s chat.",
            "Sir, you’re wearing snakeskin pants, sunglasses at night, and a cape. We’re not mad — we just want a selfie and a quick bag check.",
            "Sir, when we asked if you packed your own bag, you whispered ‘define packed.’ Please step this way."
        };

        Random rnd = new Random();
        int index = rnd.Next(tsaLines.Length);
        string selectedLine = tsaLines[index];

        return selectedLine;
    }

    public static class Icons
    {
        public const string Success = "✅";

        public const string Warning = "⚠️";

        public const string Failure = "❌";

        public const string Luggage = "🧳";

        public const string Pin = "📌";

        public const string Wrench = "🔧";

        public const string Camera = "📸";

        public const string AirplaneLiftOff = "🛫";
    }
}
