using ScmBackup.CompositionRoot;
using System;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class HosterApiCallerTests
    {
        [Fact]
        public void GetRepositoryListCallsUnderlyingHosterApi()
        {
            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            var config = reader.ReadConfig();
            var source = config.Sources.First();

            var factory = new FakeHosterFactory();
            var hoster = new FakeHoster();
            factory.FakeHoster = hoster;

            var sut = new HosterApiCaller(factory);
            sut.GetRepositoryList(source);

            Assert.True(hoster.FakeApi.WasCalled);
        }

        [Fact]
        public void ThrowsWhenConfigSourceIsNull()
        {
            var factory = new FakeHosterFactory();
            var hoster = new FakeHoster();
            factory.FakeHoster = hoster;

            var sut = new HosterApiCaller(factory);
            Assert.Throws<ArgumentNullException>(() => sut.GetRepositoryList(null));
        }
    }
}
