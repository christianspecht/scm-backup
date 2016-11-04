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
            var sut = new Context();

            var version = sut.VersionNumber;
            string versionString = sut.VersionNumberString;
            string appTitle = sut.AppTitle;
        }
    }
}
