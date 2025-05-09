public class PlaywrightDriver
{
    private static IPlaywright _playwright;
    private static IBrowser _browser;
    private static IBrowserContext _context;
    private static IPage _page;
    private static string _browserName = "chromium"; // Default
    private static bool _headless = false; // Default

    public static void SetBrowser(string browserName)
    {
        _browserName = browserName.ToLower(); 
    }
    public static void SetHeadless(bool headless)
    {
        _headless = headless;
    }

    public static async Task<IPage> GetPageAsync()
    {
        if (_page == null)
        {
            _playwright = await Playwright.CreateAsync();

            _browser = _browserName switch
            {
                "firefox" => await _playwright.Firefox.LaunchAsync(new() { Headless = _headless }),
                "webkit" => await _playwright.Webkit.LaunchAsync(new() { Headless = _headless }),
                _ => await _playwright.Chromium.LaunchAsync(new() { Headless = _headless }),
            };

            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        return _page;
    }

    public static async Task CloseAsync()
    {
        if (_browser != null)
        {
            await _browser.CloseAsync();
            _playwright?.Dispose();
            _browser = null;
            _context = null;
            _page = null;
        }
    }
}
