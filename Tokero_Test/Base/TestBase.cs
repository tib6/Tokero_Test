namespace PlaywrightTests.Base
{
    public class TestBase
    {
        protected IPage Page;
        protected ExtentTest Test;

        [SetUp]
        public async Task Setup()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            Test = ExtentTestManager.CreateTest(testName);
            Test.Info("Test started");
        }

        [TearDown]
        public async Task TearDown()
        {
            var result = TestContext.CurrentContext.Result.Outcome.Status;

            switch (result)
            {
                case NUnit.Framework.Interfaces.TestStatus.Passed:
                    Test.Pass("Test passed");
                    break;
                case NUnit.Framework.Interfaces.TestStatus.Failed:
                    Test.Fail("Test failed");
                    break;
                default:
                    Test.Skip("Test skipped");
                    break;
            }

            await PlaywrightDriver.CloseAsync();
            ExtentManager.GetExtent().Flush();
        }
    }
}