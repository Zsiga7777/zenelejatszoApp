namespace MusicApp.Configurations;

public static class ConfigureAppVariables
{
    public static MauiAppBuilder UseAppConfigurations(this MauiAppBuilder builder)
    {
        var config = new ConfigurationBuilder()
                    .Build();

        builder.Configuration.AddConfiguration(config);

        return builder;
    }
}
