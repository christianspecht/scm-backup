using System.Collections.Generic;
using Xunit;

namespace ScmBackup.Tests
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
    }
}
