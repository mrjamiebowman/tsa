using TerribleSettingsAuditor.Core.Helpers;
using TerribleSettingsAuditor.Core.Interfaces;
using TerribleSettingsAuditor.Core.Models;

namespace TerribleSettingsAuditor.Core.Services;

public class TsaCliService : ITsaCliService 
{
    // logger

    // services
    private readonly ITsaConsoleTableWriter _consoleTableWriter;

    public TsaCliService(ITsaConsoleTableWriter consoleTableWriter)
    {
        _consoleTableWriter = consoleTableWriter;
    }
    
    public void ShowReport(ScreeningReport screeningReport)
    {
        Console.WriteLine("");
        ShowBlock(" 📄 Screening Report");
        Console.WriteLine("");
        WriteGreen($" 👮 TSA: {CLI.GenerateRandomScreeningReportMessage()}");
        Console.WriteLine("");

        // sort & order
        var reports = screeningReport.Configuration.OrderBy(x => x.Pinned == true)
                                                   .ThenBy(x => x.Order).ToList();

        foreach (var item in reports)
        {
            WriteYellow($"{CLI.Icons.Luggage} Luggage: {item.Name}, ({item.Namespace})");

            List<ReportItem> reportItems = new List<ReportItem>();

            foreach (var prop in item.Properties.OrderBy(x => x.Name))
            {
                var icon = prop.Pass ? CLI.Icons.Success : CLI.Icons.Failure;
                var required = (prop.Required ?? false) ? "Yes" : "";

                // ✅ Luggage Item
                var reportItem = new ReportItem();
                reportItem.Name = prop.Name;
                reportItem.Description = prop.Description;
                reportItem.Icon = icon;
                reportItem.Description = prop.Description;
                reportItem.Secret = (prop.Secret ?? false) ? "Yes" : "";

                if (prop.Expose == true)
                {
                    // expose
                    reportItem.Expose = prop.ExposeValue;
                }

                reportItems.Add(reportItem);
            }

            // output config table
            _consoleTableWriter.WriteConfigTable(reportItems);

            Console.WriteLine("");
        }
    }

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

    public static void WriteRed(string message)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
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
        WriteGreen(@"    =  = == (____    ___D");
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
        WriteGreen($"👮 TSA: {CLI.GenerateRandomScreeningReportMessage()}");
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

        if (text.Length >= width - 2)
        {
            text = text.Substring(0, width - 4) + "..";
        }

        int padding = (width - text.Length) / 2;
        return new string(' ', padding) + text;
    }

    public static void ShowBlock(string title, string? message = null, int x = 60)
    {
        var strBlock = new string('*', x);

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
}
