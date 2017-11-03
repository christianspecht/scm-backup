using ScmBackup.CompositionRoot;
using ScmBackup.Hosters;
using System;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class HosterBackupMakerTests
    {
        [Fact]
        public void MakeBackupCallsUnderlyingMethod()
        {
            var hoster = new FakeHoster();
            hoster.FakeBackup.Result = true;

            var factory = new FakeHosterFactory(hoster);
            var repo = new HosterRepository("foo", "http://clone", ScmType.Git);

            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            var config = reader.ReadConfig();
            var source = config.Sources.First();


            var sut = new HosterBackupMaker(factory);
            var result = sut.MakeBackup(source, repo, config, "foo");

            Assert.True(hoster.FakeBackup.WasExecuted);
            Assert.Equal(hoster.FakeBackup.Result, result);
        }

        [Fact]
        public void ThrowsWhenConfigSourceIsNull()
        {
            var factory = new FakeHosterFactory(new FakeHoster());
            var repo = new HosterRepository("foo", "http://clone", ScmType.Git);

            var sut = new HosterBackupMaker(factory);
            Assert.Throws<ArgumentNullException>(() => sut.MakeBackup(null, repo, new Config(), "foo"));
        }

        [Fact]
        public void ThrowsWhenConfigIsNull()
        {
            var factory = new FakeHosterFactory(new FakeHoster());
            var repo = new HosterRepository("foo", "http://clone", ScmType.Git);

            var sut = new HosterBackupMaker(factory);
            Assert.Throws<ArgumentNullException>(() => sut.MakeBackup(new ConfigSource(), repo, null, "foo"));
        }
    }
}
