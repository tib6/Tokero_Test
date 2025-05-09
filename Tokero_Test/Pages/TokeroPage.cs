namespace Tokero_Test.Pages
{
    public class TokeroPage
    {
        private readonly IPage _page;
        public TokeroPage(IPage page) => _page = page;
        public async Task<string> GetTitleAsync() => await _page.TitleAsync();
        public async Task Nagivate() => await _page.GotoAsync(TestSettings.baseTokeroURL);
        public async Task AcceptCookies() => await _page.Locator("//button[text()='Accept all cookies']").ClickAsync();
        public async Task<IReadOnlyList<ILocator>> GetFooterElements() => await _page.Locator("//footer//span").AllAsync();
        public async Task ClickLanguageButton() => await _page.ClickAsync("//button[contains(@class,'QXn26')]/parent::div");
        public async Task SelectLanguageButton(string text) => await _page.Locator($"//div[contains(@class,'languageSwitcher')]//span[text()='{text}']").ClickAsync();
        public async Task<IReadOnlyList<ILocator>> GetLanguages() => await _page.Locator("//div[contains(@class,'languageSwitcher')]//span[text()='EN']//parent::button/following-sibling::ul/li").AllAsync();
        public async Task FooterGoTo(string text) => await _page.Locator($"//footer//a[contains(@title, '{text}') and contains(@class, 'vHH3t')]/span").First.ClickAsync();
        public async Task<IReadOnlyList<ILocator>> GetAllPolicies() => await _page.Locator("//h4").AllAsync();

    }
}
