namespace PlaywrightTests.Hooks
{
    public class ExtentTestManager
    {
        private static ConcurrentDictionary<string, ExtentTest> _featureMap = new();
        private static ConcurrentDictionary<string, ExtentTest> _testMap = new();

        public static ExtentTest CreateTest(string testName)
        {
            var test = ExtentManager.GetExtent().CreateTest(testName);
            _testMap[testName] = test;
            return test;
        }

        public static ExtentTest GetTest(string testName)
        {
            return _testMap[testName];
        }
    }
}