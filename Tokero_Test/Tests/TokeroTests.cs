namespace Tokero_Test.Tests
{
    [TestFixture]
    public class TokeroTests : TestBase
    {
        public bool headless = true;

        [Test]
        [Repeat(2)]
        [TestCase("chromium")]
        [TestCase("webkit")]
        public async Task VerifyPolicyTitleTime(string browser)
        {
            var stopwatch = Stopwatch.StartNew();
            PlaywrightDriver.SetHeadless(headless);
            PlaywrightDriver.SetBrowser(browser);
            Page = await PlaywrightDriver.GetPageAsync();
            var tokeroPage = new TokeroPage(Page);
            await tokeroPage.Nagivate();
            await tokeroPage.AcceptCookies();
            await tokeroPage.FooterGoTo("Policies list");
            var pageTitle = Page.Locator("//h1");
            await pageTitle.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            bool isVisible = await pageTitle.IsVisibleAsync();
            Assert.That(isVisible, Is.True, "Polices Title nu a fost gasit sau incarcat corect.");

            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            Test.Info($"Page loaded in {elapsedMs} ms in: " + browser);

            Assert.That(elapsedMs, Is.LessThan(6500), "Page load took too long");
        }

        [Test]
        [TestCase("chromium")]
        [TestCase("webkit")]
        public async Task VerifyPolicyPageTitles(string browser)
        {
            PlaywrightDriver.SetHeadless(headless);
            PlaywrightDriver.SetBrowser(browser);
            Page = await PlaywrightDriver.GetPageAsync();
            var tokeroPage = new TokeroPage(Page);
            await tokeroPage.Nagivate();
            await tokeroPage.AcceptCookies();
            await tokeroPage.FooterGoTo("Policies list");
            var pageTitle = Page.Locator("//h1");
            await pageTitle.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            bool isVisible = await pageTitle.IsVisibleAsync();
            Assert.That(isVisible, Is.True, "Polices Title nu a fost gasit sau incarcat corect.");
            var allPolicies = await tokeroPage.GetAllPolicies();

            foreach (var police in allPolicies)
            {
                var policeName = await police.TextContentAsync();
                Console.WriteLine(policeName);
                await police.ClickAsync();
                await pageTitle.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
                isVisible = await pageTitle.IsVisibleAsync();
                Test.Info(policeName + " :" + isVisible);
                Assert.That(isVisible, Is.True);
                await Page.GoBackAsync();
            }
        }

        [TestCase("chromium")]
        [TestCase("firefox")]
        [TestCase("webkit")]
        public async Task MultiLanguageHomePageTest(string browser)
        {
            PlaywrightDriver.SetHeadless(headless);
            PlaywrightDriver.SetBrowser(browser);
            Page = await PlaywrightDriver.GetPageAsync();
            var tokeroPage = new TokeroPage(Page);
            await tokeroPage.Nagivate();
            await tokeroPage.AcceptCookies();
            Assert.That(Page.Url.Contains("en"), "Pagina curenta cu contine en");
            Console.WriteLine("EN");
            Test.Info($"Finalizat pentru: EN");
            await tokeroPage.ClickLanguageButton();
            var allLanguages = await tokeroPage.GetLanguages();
            await tokeroPage.ClickLanguageButton();

            var texts = (await Task.WhenAll(allLanguages.Select(l => l.TextContentAsync())))
                         .Where(t => t != null && !string.IsNullOrWhiteSpace(t))
                         .Select(t => t!.Trim()) 
                         .ToList();

            foreach (var language in texts)
            {
                await tokeroPage.ClickLanguageButton();
                await tokeroPage.SelectLanguageButton(language);
                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                Assert.That(Page.Url.Contains(language.ToLower()), $"{language} nu corespunde cu url: {Page.Url}");
                Console.WriteLine(language);
                Test.Info($"Finalizat pentru: {language}");
            }
        }
    }
}

