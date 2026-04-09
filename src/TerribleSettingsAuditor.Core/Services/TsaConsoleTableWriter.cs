using TerribleSettingsAuditor.Core.Interfaces;
using TerribleSettingsAuditor.Core.Models;

namespace TerribleSettingsAuditor.Core.Services;

public class TsaConsoleTableWriter : ITsaConsoleTableWriter
{
    public TsaConsoleTableWriter()
    {

    }

    private string[] Headers = new[] { "Luggage Item", "Secret", "Expose", "State" };

    public void WriteConfigTable(List<ReportItem> reportItems)
    {
        if (Headers == null || Headers.Length == 0) {
            throw new ArgumentException("Headers are required.", nameof(Headers));
        }

        // flatten values
        var flattenedItems = reportItems.Select(item => new string[]
        {
            item.Name ?? string.Empty,
            item.Secret ?? string.Empty,
            item.Expose ?? string.Empty,
            item.State ?? string.Empty
        }).ToList();

        // column widths
        int[] widths = GetColumnWidths(Headers, flattenedItems);

        WriteBorder(widths);
        WriteRow(Headers, widths);
        WriteBorder(widths);

        foreach (var row in reportItems)
        {
            var flattenedRow = new string[]
            {
                row.Name ?? string.Empty,
                row.Secret ?? string.Empty,
                row.Expose ?? string.Empty,
                row.State ?? string.Empty
            };

            WriteRow(flattenedRow, widths);
        }

        WriteBorder(widths);
    }

    private static int[] GetColumnWidths(string[] headers, List<string[]> rows)
    {
        int[] widths = new int[headers.Length];

        for (int i = 0; i < headers.Length; i++)
        {
            widths[i] = headers[i]?.Length ?? 0;
        }

        foreach (var row in rows)
        {
            for (int i = 0; i < row.Length; i++)
            {
                widths[i] = Math.Max(widths[i], row[i]?.Length ?? 0);
            }
        }

        for (int i = 0; i < widths.Length; i++)
        {
            widths[i] += 2;
        }

        return widths;
    }

    private static void WriteBorder(int[] widths)
    {
        Console.Write("+");

        foreach (int width in widths)
        {
            Console.Write(new string('-', width));
            Console.Write("+");
        }

        Console.WriteLine();
    }

    private static void WriteRow(string[] values, int[] widths)
    {
        Console.Write("|");

        for (int i = 0; i < values.Length; i++)
        {
            string value = values[i] ?? string.Empty;
            Console.Write(" " + value.PadRight(widths[i] - 1));
            Console.Write("|");
        }

        Console.WriteLine();
    }
}