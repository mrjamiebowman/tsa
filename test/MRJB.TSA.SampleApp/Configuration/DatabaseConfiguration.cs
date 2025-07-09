using MRJB.TSA.Abstractions.Attribute;
using MRJB.TSA.Abstractions.Enums;

namespace MRJB.TSA.SampleApp.Configuration;

[CarryOn("Databases", "Database Connection strings", true)]
public class DatabaseConfiguration
{
    public const string Position = "Database";

    [BaggageItem("SampleApp", "SampleApp Connection String", true)]
    [BaggageItemConnectionString(ConnectionStringEnum.SqlServer)]
    public string? ConnectionStringSampleApp { get; set; }

    [BaggageItem("UsersDb", "UsersDb Connection String", true)]
    [BaggageItemConnectionString(ConnectionStringEnum.SqlServer)]
    public string? ConnectionStringUsersDb { get; set; }
}
