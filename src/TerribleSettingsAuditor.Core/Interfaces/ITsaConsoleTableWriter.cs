using TerribleSettingsAuditor.Core.Models;

namespace TerribleSettingsAuditor.Core.Interfaces;

public interface ITsaConsoleTableWriter
{
    void WriteConfigTable(List<ReportItem> reportItems);
}
