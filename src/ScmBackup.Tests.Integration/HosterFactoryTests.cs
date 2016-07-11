using ScmBackup.Hosters;
using ScmBackup.Tests.Hosters;
using System;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class HosterFactoryTests
    {
        private readonly HosterFactory sut;
        private readonly IHoster hoster;

        public HosterFactoryTests()
        {
            sut = new HosterFactory();
            hoster = new FakeHoster();
            sut.Add(hoster);
        }

        [Fact]
        public void NewHosterIsAdded()
        {
            Assert.Equal(1, sut.Count);
            Assert.Equal(hoster, sut["fake"]);
        }

        [Fact]
        public void CreateReturnsExistingHoster()
        {
            var result = sut.Create("fake");

            Assert.NotNull(result);
            Assert.Equal(hoster, result);
        }

        [Fact]
        public void CreateThrowsWhenGivenNonExistingHoster()
        {
            Assert.ThrowsAny<Exception>(() => sut.Create("foo"));
        }
    }
}
