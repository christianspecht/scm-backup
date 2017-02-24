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

            var result = HosterNameHelper.GetHosterName(t, "hoster");

            Assert.Equal("fake", result);
        }

        [Fact]
        public void ThrowsWhenTypeNameDoesntEndWithSuffix()
        {
            var t = typeof(FakeHosterApi);

            Assert.Throws<InvalidOperationException>(() => HosterNameHelper.GetHosterName(t, "hoster"));
        }

        [Fact]
        public void ThrowsWhenTypeNameDoesntContainSuffix()
        {
            var t = typeof(FooBarBar);

            Assert.Throws<InvalidOperationException>(() => HosterNameHelper.GetHosterName(t, "hoster"));
        }

        [Fact]
        public void ThrowsWhenTypeNameContainsSuffixMoreThanOnce()
        {
            var t = typeof(FooBarBar);

            Assert.Throws<InvalidOperationException>(() => HosterNameHelper.GetHosterName(t, "bar"));
        }
    }
}
