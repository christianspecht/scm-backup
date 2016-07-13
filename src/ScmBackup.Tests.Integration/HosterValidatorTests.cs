using ScmBackup.CompositionRoot;
using ScmBackup.Tests.Hosters;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Integration
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

            var hoster = new FakeHoster();
            var factory = new HosterFactory();
            factory.Add(hoster);

            var sut = new HosterValidator(factory);
            sut.Validate(source);

            Assert.True(hoster.FakeValidator.WasValidated);
        }
    }
}
