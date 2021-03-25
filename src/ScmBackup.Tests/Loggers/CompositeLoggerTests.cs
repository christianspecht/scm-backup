using ScmBackup.Loggers;
using System.Collections.Generic;
using Xunit;

namespace ScmBackup.Tests.Loggers
{
    public class CompositeLoggerTests
    {
        [Fact]
        public void CallsAllUnderlyingLoggers()
        {
            var loggers = new List<FakeLogger> { new FakeLogger(), new FakeLogger() };
            var sut = new CompositeLogger(loggers);

            sut.Log(ErrorLevel.Info, "foo");

            foreach(var mock in loggers)
            {
                Assert.True(mock.LoggedSomething);
                Assert.Equal(ErrorLevel.Info, mock.LastErrorLevel);
                Assert.Equal("foo", mock.LastMessage);
            }

        }

        [Fact]
        public void ExecutesAllUnderlyingLoggers()
        {
            var logger1 = new FakeLogger();
            var logger2 = new FakeLogger();

            var loggers = new List<FakeLogger> { logger1, logger2 };
            var sut = new CompositeLogger(loggers);
            sut.ExecuteOnExit(true);

            Assert.True(logger1.ExecutedOnExit && logger2.ExecutedOnExit);
        }

        [Fact]
        public void ReturnsFilesFromAllUnderlyingLoggers()
        {
            var logger1 = new FakeLogger();
            logger1.FakeFilesToBackup = new List<string> { "a.txt", "b.txt" };

            var logger2 = new FakeLogger();
            logger2.FakeFilesToBackup = new List<string> { "c.txt" };

            var loggers = new List<FakeLogger> { logger1, logger2 };
            var sut = new CompositeLogger(loggers);
            var list = sut.FilesToBackup;

            Assert.Equal(3, list.Count);
            Assert.Contains("a.txt", list);
            Assert.Contains("b.txt", list);
            Assert.Contains("c.txt", list);
        }
    }
}
