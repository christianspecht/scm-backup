using ScmBackup.Hosters;
using System;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class HosterNameHelperTests
    {
        [Fact]
        public void ReturnsHosterName()
        {
            var t = typeof(FakeHoster);

            var sut = new HosterNameHelper();

            var result = sut.GetHosterName(t, "hoster");

            Assert.Equal("fake", result);
        }

        [Fact]
        public void ThrowsWhenTypeNameDoesntEndWithSuffix()
        {
            var t = typeof(FakeHosterApi);

            var sut = new HosterNameHelper();

            Assert.Throws<InvalidOperationException>(() => sut.GetHosterName(t, "hoster"));
        }
    }
}
