using ScmBackup.Hosters;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests
{
    public class ApiRepositoriesTests
    {
        [Fact]
        public void AddAddsItem()
        {
            var source = new ConfigSource();
            source.Title = "title";

            var list = new List<HosterRepository>();

            var sut = new ApiRepositories();
            sut.AddItem(source, list);

            Assert.Equal(1, sut.Dic.Count);
        }

        [Fact]
        public void GetSourcesReturnsSources()
        {
            var source1 = new ConfigSource();
            source1.Title = "testsource";
            var source2 = new ConfigSource();
            source2.Title = "testsource2";

            var list = new List<HosterRepository>();
            list.Add(new HosterRepository("foo", "http://foo", ScmType.Git));

            var sut = new ApiRepositories();
            sut.AddItem(source1, list);
            sut.AddItem(source2, list);

            var result = sut.GetSources();

            Assert.Equal(2, result.Count());
            Assert.True(result.Contains(source1));
            Assert.True(result.Contains(source2));
        }

        [Fact]
        public void GetReposForSourceReturnsList()
        {
            var source = new ConfigSource();
            source.Title = "title";

            var list = new List<HosterRepository>();
            list.Add(new HosterRepository("foo", "http://foo", ScmType.Git));
            list.Add(new HosterRepository("bar", "http://bar", ScmType.Git));

            var sut = new ApiRepositories();
            sut.AddItem(source, list);

            var result = sut.GetReposForSource(source);

            Assert.Equal(2, result.Count);
            Assert.Equal("foo", result.First().Name);
        }

        [Fact]
        public void GetScmTypesReturnsHashSet()
        {
            // TODO: use different ScmTypes when more are supported
            var repos = new List<HosterRepository>();
            repos.Add(new HosterRepository("testrepo", "http://clone.url", ScmType.Git));
            repos.Add(new HosterRepository("testrepo2", "http://clone2.url", ScmType.Git));

            var source1 = new ConfigSource();
            source1.Title = "testsource";
            var source2 = new ConfigSource();
            source2.Title = "testsource2";

            var sut = new ApiRepositories();
            sut.AddItem(source1, repos);
            sut.AddItem(source2, repos);

            var result = sut.GetScmTypes();

            Assert.Equal(1, result.Count);
            Assert.Equal(ScmType.Git, result.First());
        }

        [Fact]
        public void GetScmTypesThrowsWhenEmpty()
        {
            var sut = new ApiRepositories();

            Assert.Throws<InvalidOperationException>(() => sut.GetScmTypes());
        }
    }
}
