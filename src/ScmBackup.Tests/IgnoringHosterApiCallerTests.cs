using ScmBackup.Hosters;
using ScmBackup.Tests.Hosters;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests
{
    public class IgnoringHosterApiCallerTests
    {
        ConfigSource source;
        IgnoringHosterApiCaller sut;

        public IgnoringHosterApiCallerTests()
        {
            this.source = new ConfigSource { Title = "foo" };

            var repos = new List<HosterRepository>();
            repos.Add(new HosterRepository("foo.bar", "bar", "http://clone", ScmType.Git));
            repos.Add(new HosterRepository("foo.baz", "baz", "http://clone", ScmType.Git));

            var caller = new FakeHosterApiCaller();
            caller.Lists.Add(this.source, repos);

            this.sut = new IgnoringHosterApiCaller(caller);
        }

        [Fact]
        public void IgnoresRepo()
        {
            this.source.IgnoreRepos = new List<string> { "bar" };

            var list = this.sut.GetRepositoryList(this.source);

            Assert.Equal(1, list.Count);
            Assert.Equal("baz", list.First().ShortName);
        }

        [Fact]
        public void DoesntIgnoreWhenNotSet()
        {
            // don't set this.source.IgnoreRepos

            var list = this.sut.GetRepositoryList(this.source);

            Assert.Equal(2, list.Count);
        }
    }
}
