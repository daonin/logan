using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidLogFileName_BadExtention_ReturnsFalse()
        {
            var logAnalyzer = new LogAnalyzer();

            bool result = logAnalyzer.IsValidLogFileName("log.txt");

            Assert.False(result);
        }
    }
}
