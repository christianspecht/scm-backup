using ScmBackup.CompositionRoot;
using System;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class HosterValidatorTests
    {
        [Fact]
        public void ValidateCallsUnderlyingValidator()
        {
            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            var config = reader.ReadConfig();
            var source = config.Sources.First();

            var factory = new FakeHosterFactory();
            var hoster = new FakeHoster();
            factory.FakeHoster = hoster;

            var sut = new HosterValidator(factory);
            sut.Validate(source);

            Assert.True(hoster.FakeValidator.WasValidated);
        }

        [Fact]
        public void ThrowsWhenConfigSourceIsNull()
        {
            var factory = new FakeHosterFactory();
            var hoster = new FakeHoster();
            factory.FakeHoster = hoster;

            var sut = new HosterValidator(factory);
            Assert.Throws<ArgumentNullException>(() => sut.Validate(null));
        }
    }
}
