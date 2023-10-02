using Npgsql;

namespace Infrastructure;

public class Utilities
{
    public static readonly Uri Uri;
    public static readonly string FormattedConnectionString;

    static Utilities()
    {
        string rawConnectionString;
        string envVarKeyName = "pgconn";

        rawConnectionString = Environment.GetEnvironmentVariable(envVarKeyName)!;
        if (rawConnectionString == null)
        {
            throw new Exception($@"YOUR CONN STRING {envVarKeyName} IS EMPTY.");
        }

        try
        {
            Uri = new Uri(rawConnectionString);
            FormattedConnectionString = string.Format(
                "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=True;MaxPoolSize=3",
                Uri.Host,
                Uri.AbsolutePath.Trim('/'),
                Uri.UserInfo.Split(':')[0],
                Uri.UserInfo.Split(':')[1],
                Uri.Port > 0 ? Uri.Port : 5432);
            new NpgsqlDataSourceBuilder(FormattedConnectionString).Build().OpenConnection().Close();
        }
        catch (Exception e)
        {
            throw new Exception("Your connection string is found, but could not be used.", e);
        }
    }
}