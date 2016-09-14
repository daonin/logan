using System;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    internal class FakeExtentionManager : IExtentionManager
    {
        public bool WillBeValid = false;

        public bool IsValid(string fileName)
        {
            return WillBeValid;

        }
    }

    internal class TestableLogAnalyzer : LogAnalyzer
    {
        private IExtentionManager _manager;

        public TestableLogAnalyzer(IExtentionManager manager)
        {
            _manager = manager;
        }
        public override IExtentionManager GetExtentionManager()
        {
            return _manager;
        }
    }
    [TestFixture]
    public class LogAnalyzerTests
    {
        private LogAnalyzer MakeAnalyzer()
        {
            return new LogAnalyzer();
        }

        [TearDown]
        public void ExtentionManagerFactoryTearDown()
        {
            ExtentionManagerFactory.SetManager(null);
        }

        [Test]
        public void IsValidLogFileName_BadExtention_ReturnsFalse()
        {
            var manager = new FakeExtentionManager();
            manager.WillBeValid = false;
            ExtentionManagerFactory.SetManager(manager);

            var logAnalyzer = MakeAnalyzer();

            bool result = logAnalyzer.IsValidLogFileName("log.txt");

            Assert.False(result);
        }

        [TestCase("log.slf")]
        [TestCase("log.SLF")]
        public void IsValidLogFileName_ValidExtentions_ReturnsTrue(string file)
        {
            var manager = new FakeExtentionManager();
            manager.WillBeValid = true;
            ExtentionManagerFactory.SetManager(manager);

            var logAnalyzer = MakeAnalyzer();

            bool result = logAnalyzer.IsValidLogFileName(file);

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_EmptyFileName_ThrowsException()
        {
            var logAnalyzer = MakeAnalyzer();

            var ex = Assert.Catch<Exception>(() => 
                logAnalyzer.IsValidLogFileName(""));

            StringAssert.Contains("Filename has to be provided.", ex.Message);
        }

        [TestCase(false, false)]
        [TestCase(true, true)]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid(bool willBeValidParam, bool expectedWasLastFileNameValidValue)
        {
            FakeExtentionManager fakeExtentionManager = new FakeExtentionManager();
            fakeExtentionManager.WillBeValid = willBeValidParam;
            ExtentionManagerFactory.SetManager(fakeExtentionManager);

            var logAnalyzer = MakeAnalyzer();

            logAnalyzer.IsValidLogFileName("somefile.foo");

            Assert.AreEqual(logAnalyzer.WasLastFileNameValid, expectedWasLastFileNameValidValue);
        }

        [Test]
        public void IsValidLogFileName_SupportedExtention_ReturnsTrue()
        {
            FakeExtentionManager fakeExtentionManager = new FakeExtentionManager();
            fakeExtentionManager.WillBeValid = true;

            var logAnalyzer = new TestableLogAnalyzer(fakeExtentionManager);
            
            bool result = logAnalyzer.IsValidLogFileName("validlog.foo");

            Assert.True(result);
        }
    }
}
