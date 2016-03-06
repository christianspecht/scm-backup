using System.Linq;
using Xunit;

namespace ScmBackup.Tests
{
    public class ValidationResultTests
    {
        [Fact]
        public void AddMessageAddsSingleMessage()
        {
            var sut = new ValidationResult();
            sut.AddMessage(ErrorLevel.Info, "i");

            Assert.Equal(1, sut.Messages.Count);
        }

        [Fact]
        public void IsValidIsTrueWithNoMessages()
        {
            var sut = new ValidationResult();

            Assert.False(sut.Messages.Any());
            Assert.True(sut.IsValid);
        }

        [Fact]
        public void IsValidIsTrueWithInfoAndWarnMessage()
        {
            var sut = new ValidationResult();
            sut.AddMessage(ErrorLevel.Info, "i");
            sut.AddMessage(ErrorLevel.Warn, "w");

            Assert.Equal(2, sut.Messages.Count);
            Assert.True(sut.IsValid);
        }

        [Fact]
        public void IsValidIsFalseWithErrorMessage()
        {
            var sut = new ValidationResult();
            sut.AddMessage(ErrorLevel.Error, "e");

            Assert.Equal(1, sut.Messages.Count);
            Assert.False(sut.IsValid);
        }

        [Fact]
        public void IsValidIsFalseWithAllMessages()
        {
            var sut = new ValidationResult();
            sut.AddMessage(ErrorLevel.Info, "i");
            sut.AddMessage(ErrorLevel.Warn, "w");
            sut.AddMessage(ErrorLevel.Error, "e");

            Assert.Equal(3, sut.Messages.Count);
            Assert.False(sut.IsValid);
        }
    }
}
