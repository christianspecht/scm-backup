using ScmBackup.Configuration;
using ScmBackup.Hosters;
using ScmBackup.Tests.Hosters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ScmBackup.Tests
{
    public class IncludingHosterApiCallerTests
    {
        ConfigSource source;
        IncludingHosterApiCaller sut;

        public IncludingHosterApiCallerTests()
        {
            this.source = new ConfigSource { Title = "foo" };

            var repos = new List<HosterRepository>();
            repos.Add(new HosterRepository("foo.bar", "bar", "http://clone", ScmType.Git));
            repos.Add(new HosterRepository("foo.baz", "baz", "http://clone", ScmType.Git));

            var caller = new FakeHosterApiCaller();
            caller.Lists.Add(this.source, repos);

            this.sut = new IncludingHosterApiCaller(caller);
        }

        [Fact]
        public void IncludesRepo()
        {
            this.source.IncludeRepos = new List<string> { "bar" };

            var list = this.sut.GetRepositoryList(this.source);

            Assert.Single(list);
            Assert.Equal("bar", list.First().ShortName);
        }

        [Fact]
        public void BackupsEverythingWhenNotSet()
        {
            // don't set this.source.IncludeRepos

            var list = this.sut.GetRepositoryList(this.source);

            Assert.Equal(2, list.Count);
        }
    }
}
