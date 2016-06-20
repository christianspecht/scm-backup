using ScmBackup.Hosters;
using ScmBackup.Tests.Hosters;
using System;
using Xunit;

namespace ScmBackup.Tests
{
    public class HosterFactoryTests
    {
        [Fact]
        public void NewHosterIsAdded()
        {
            var sut = new HosterFactory();
            var hoster = new FakeHoster();

            sut.Add(hoster);

            Assert.Equal(1, sut.Count);
            Assert.Equal(hoster, sut["fake"]);
        }

        [Fact]
        public void CreateReturnsExistingHoster()
        {
            var sut = new HosterFactory();
            var hoster = new FakeHoster();
            sut.Add(hoster);

            var result = sut.Create("fake");

            Assert.NotNull(result);
            Assert.Equal(hoster, result);
        }

        [Fact]
        public void CreateThrowsWhenGivenNonExistingHoster()
        {
            var sut = new HosterFactory();
            var hoster = new FakeHoster();
            sut.Add(hoster);

            Assert.ThrowsAny<Exception>(() => sut.Create("foo"));
        }
    }
}
