using ScmBackup.Loggers;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests
{
    public class MessageLoggerTests
    {
        [Fact]
        public void LogsMessage()
        {
            var msg = new LogMessages();
            var sut = new MessageLogger(msg);

            sut.Log(ErrorLevel.Info, "foo {0}", "bar");

            var result = msg.GetMessages().ToList();
            Assert.Equal(1, result.Count);

            string text = result.First();

            Assert.Contains("foo", text);
            Assert.Contains("bar", text);
            Assert.Contains("Info", text);
        }
    }
}
