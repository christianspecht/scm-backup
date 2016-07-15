using ScmBackup.CompositionRoot;
using ScmBackup.Hosters;
using ScmBackup.Tests.Hosters;
using SimpleInjector;
using System;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class HosterFactoryTests
    {
        private readonly HosterFactory sut;

        public HosterFactoryTests()
        {
            sut = new HosterFactory(new Container());
            sut.Register<FakeHoster>();
        }

        [Fact]
        public void NewHosterIsAdded()
        {
            Assert.Equal(1, sut.Count);
        }

        [Fact]
        public void CreateReturnsHoster()
        {
            var result = sut.Create("fake");

            Assert.NotNull(result);
            Assert.True(result is IHoster);
        }

        [Fact]
        public void CreateThrowsWhenGivenNonExistingHoster()
        {
            Assert.ThrowsAny<Exception>(() => sut.Create("foo"));
        }
    }
}
