using ScmBackup.Resources;
using System;
using System.Globalization;
using Xunit;

namespace ScmBackup.Tests.Resources
{
    public class ResourceProviderTests
    {
        [Fact]
        public void ThrowsWhenNotInitialized()
        {
            var sut = new ResourceProvider();
            Assert.Throws<InvalidOperationException>(() => sut.GetString("foo"));
        }

        [Fact]
        public void ThrowsWhenInitalizedWithoutCulture()
        {
            var sut = new ResourceProvider();
            Assert.Throws<InvalidOperationException>(() => sut.Initialize(null));
        }

        [Fact]
        public void ReturnsStringWhenInitializedProperly()
        {
            var sut = new ResourceProvider();
            sut.Initialize(new CultureInfo("en-US"));
            string result = sut.GetString("AppTitle");

            Assert.NotNull(result);
        }
    }
}
