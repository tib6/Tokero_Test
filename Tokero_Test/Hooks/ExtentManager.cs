namespace PlaywrightTests.Hooks
{
    public class ExtentManager
    {
        private static ExtentReports _extent;
        private static ExtentSparkReporter _spark;

        public static ExtentReports GetExtent()
        {
            if (_extent == null)
            {
                string reportsDir = Path.Combine(AppContext.BaseDirectory, "Reports");

                // Creează directorul dacă nu există
                if (!Directory.Exists(reportsDir))
                {
                    Directory.CreateDirectory(reportsDir);
                }

                string reportPath = Path.Combine(reportsDir, $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");

                _spark = new ExtentSparkReporter(reportPath);
                _extent = new ExtentReports();
                _extent.AttachReporter(_spark);
            }
            return _extent;
        }
    }
}
