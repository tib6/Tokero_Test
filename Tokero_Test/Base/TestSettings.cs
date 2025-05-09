public class TestSettings
{
    private static readonly Settings settings;
    static TestSettings()
    {
        settings ??= new Settings();
    }

    //Tokero
    public static string baseTokeroURL => settings.GetConfig().Tokero.baseURL.ToString();
}

