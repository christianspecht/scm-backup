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
            Assert.NotEmpty(result.Messages);

            var message = result.Messages.First(r => r.Type == ValidationMessageType.WrongHoster);
            Assert.Equal(ErrorLevel.Error, message.Error);
        }

        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        public void ReturnsErrorWhenTypeIsInvalid(string value)
        {
            config.Type = value;

            var result = sut.Validate(config);

            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Messages);

            var message = result.Messages.First(r => r.Type == ValidationMessageType.WrongType);
            Assert.Equal(ErrorLevel.Error, message.Error);
        }

        [Theory]
        [InlineData("user")]
        [InlineData("org")]
        public void ValidatesWhenTypeIsValid(string value)
        {
            config.Type = value;
            
            var result = sut.Validate(config);

            Assert.True(result.IsValid);
            Assert.Empty(result.Messages);
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
            Assert.NotEmpty(result.Messages);

            var message = result.Messages.First(r => r.Type == ValidationMessageType.NameEmpty);
            Assert.Equal(ErrorLevel.Error, message.Error);

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

            Assert.NotEmpty(result.Messages);

            var message = result.Messages.First(r => r.Type == ValidationMessageType.AuthNameOrPasswortEmpty);
            Assert.Equal(ErrorLevel.Error, message.Error);
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
            Assert.NotEmpty(result.Messages);

            var message = result.Messages.First(r => r.Type == ValidationMessageType.AuthNameAndPasswortEmpty);
            Assert.Equal(ErrorLevel.Warn, message.Error);
        }

        [Fact]
        public void AuthNameAndName_AreNotEqual()
        {
            config.Name = "foo";
            config.AuthName = "bar";
            var result = sut.Validate(config);

            var message = result.Messages.FirstOrDefault(r => r.Type == ValidationMessageType.AuthNameAndNameNotEqual);

            if (sut.AuthNameAndNameMustBeEqual)
            {
                Assert.NotEmpty(result.Messages);
                Assert.NotNull(message);               
                Assert.Equal(ErrorLevel.Warn, message.Error);
            }
            else
            {
                Assert.NotNull(message);
            }
        }

        [Fact]
        public void AuthNameAndName_NotEqualDoesntMatterForOrgs()
        {
            config.Type = "org";
            config.Name = "foo";
            config.AuthName = "bar";
            var result = sut.Validate(config);

            var message = result.Messages.FirstOrDefault(r => r.Type == ValidationMessageType.AuthNameAndNameNotEqual);

            Assert.Null(message);
        }
    }
}
