using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer logAnalyzer;

        [SetUp]
        public void LogAnalyzerTestsSetUp()
        {
            logAnalyzer = new LogAnalyzer();
        }

        [Test]
        public void IsValidLogFileName_BadExtention_ReturnsFalse()
        {   
            bool result = logAnalyzer.IsValidLogFileName("log.txt");

            Assert.False(result);
        }

        [TestCase("log.slf")]
        [TestCase("log.SLF")]
        public void IsValidLogFileName_ValidExtentions_ReturnsTrue(string file)
        {
            bool result = logAnalyzer.IsValidLogFileName(file);

            Assert.True(result);
        }
    }
}
