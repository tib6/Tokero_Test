public class Settings
{
    private readonly dynamic config;
    public Settings()
    {
#if DEBUG
        Console.WriteLine("in debug");
        config ??= Newtonsoft.Json.Linq.JObject.Parse(File.ReadAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}/../../../appSettings.json"));
#else
            Console.WriteLine("in production");	
            config ??= JObject.Parse(File.ReadAllText($"{System.AppDomain.CurrentDomain.BaseDirectory}/ui.tests.settings.Production.json"));	
#endif
    }
    public dynamic GetConfig()
    {
        return config;
    }
}

