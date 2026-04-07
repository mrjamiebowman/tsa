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
        if (Headers == null || Headers.Length == 0)
            throw new ArgumentException("Headers are required.", nameof(Headers));

        // flatten values
        var flattenedItems = reportItems.Select(item => new string[]
        {
            item.Name ?? string.Empty,
            item.Secret ?? string.Empty,
            item.Expose ?? string.Empty,
            item.State ?? string.Empty
        }).ToList();

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

    //public void Write(string[] headers, IEnumerable<string[]> rows)
    //{
    //    if (headers == null || headers.Length == 0)
    //        throw new ArgumentException("Headers are required.", nameof(headers));

    //    var rowList = rows?.ToList() ?? new List<string[]>();

    //    if (rowList.Any(r => r.Length != headers.Length))
    //        throw new ArgumentException("All rows must have the same number of columns as headers.", nameof(rows));

    //    int[] widths = GetColumnWidths(headers, rowList);

    //    WriteBorder(widths);
    //    WriteRow(headers, widths);
    //    WriteBorder(widths);

    //    foreach (var row in rowList)
    //    {
    //        WriteRow(row, widths);
    //    }

    //    WriteBorder(widths);
    //}

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

        //// icon column fixed width
        //widths[0] = 3;

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