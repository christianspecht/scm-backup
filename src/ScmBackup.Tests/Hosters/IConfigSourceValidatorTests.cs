using ScmBackup.Hosters;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public abstract class IConfigSourceValidatorTests
    {
        // these need to be created in the child classes' constructor
        internal ConfigSource config;
        internal IConfigSourceValidator sut;

        [Fact]
        public void PropertiesWereSetInChildClass()
        {
            Assert.NotNull(config);
            Assert.NotNull(sut);
        }

        [Fact]
        public void ReturnsErrorWhenHosterIsWrong()
        {
            config.Hoster = "foo";
            
            var result = sut.Validate(config);

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Error, result.Messages[0].Error);
        }

        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        public void ReturnsErrorWhenTypeIsInvalid(string value)
        {
            config.Type = value;

            var result = sut.Validate(config);

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Error, result.Messages[0].Error);
        }

        [Theory]
        [InlineData("user")]
        [InlineData("org")]
        public void ValidatesWhenTypeIsValid(string value)
        {
            config.Type = value;
            
            var result = sut.Validate(config);

            Assert.True(result.IsValid);
            Assert.False(result.Messages.Any());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ReturnsErrorWhenNameIsEmpty(string value)
        {
            config.Name = value;
            
            var result = sut.Validate(config);

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Error, result.Messages[0].Error);
        }

        [Theory]
        [InlineData("user", "")]
        [InlineData("user", null)]
        [InlineData("", "password")]
        [InlineData("", "null")]
        public void ReturnsErrorWhenAuthNameORPasswordAreSet(string user, string password)
        {
            config.AuthName = user;
            config.Password = password;
            
            var result = sut.Validate(config);

            Assert.False(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Error, result.Messages[0].Error);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ReturnsWarningWhenAuthNameAndPasswordAreEmpty(string value)
        {
            config.AuthName = value;
            config.Password = value;
            
            var result = sut.Validate(config);

            Assert.True(result.IsValid);
            Assert.Equal(1, result.Messages.Count);
            Assert.Equal(ErrorLevel.Warn, result.Messages[0].Error);
        }
    }
}
