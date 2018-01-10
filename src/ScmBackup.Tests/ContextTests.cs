using Xunit;

namespace ScmBackup.Tests
{
    public class ContextTests
    {
        [Fact]
        public void DoesNotThrowExceptions()
        {
            // most of the Context class is .NET Framework functionality (which we don't want to test again),
            // but at least we want to be noticed when anything throws an exception

            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            var sut = new Context(reader);

            var version = sut.VersionNumber;
            string versionString = sut.VersionNumberString;
            string appTitle = sut.AppTitle;
        }

        [Fact]
        public void UsesConfigFromReader()
        {
            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            var sut = new Context(reader);

            Assert.NotNull(sut.Config);

            // This checks reference equality (not content), but in this case it's good enough.
            // We just want to know whether the context returns the config that came from the IConfigReader.
            Assert.Equal(reader.FakeConfig, sut.Config);
        }
    }
}
