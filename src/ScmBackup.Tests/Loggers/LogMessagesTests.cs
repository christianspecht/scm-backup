using ScmBackup.Loggers;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Loggers
{
    public class LogMessagesTests
    {
        [Fact]
        public void AddsMessagesInCorrectOrder()
        {
            var sut = new LogMessages();
            sut.AddMessage("0");
            sut.AddMessage("1");

            var result = sut.GetMessages().ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("0", result[0]);
            Assert.Equal("1", result[1]);
        }
    }
}
