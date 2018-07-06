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
            list.Add(new HosterRepository("foo", "foo", "http://foo", ScmType.Git));

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
            list.Add(new HosterRepository("bar", "bar", "http://bar", ScmType.Git));
            list.Add(new HosterRepository("foo", "foo", "http://foo", ScmType.Git));

            var sut = new ApiRepositories();
            sut.AddItem(source, list);

            var result = sut.GetReposForSource(source);

            Assert.Equal(2, result.Count());
            Assert.Equal("bar", result.First().FullName);
        }

        [Fact]
        public void GetReposForSourceReturnsAlphabeticallySorted()
        {
            var source = new ConfigSource();
            source.Title = "title";

            var list = new List<HosterRepository>();
            list.Add(new HosterRepository("ccc", "ccc", "http://ccc", ScmType.Git));
            list.Add(new HosterRepository("aaa", "aaa", "http://aaa", ScmType.Git));
            list.Add(new HosterRepository("bbb", "bbb", "http://bbb", ScmType.Git));

            var sut = new ApiRepositories();
            sut.AddItem(source, list);

            var result = sut.GetReposForSource(source).ToList();

            Assert.Equal("aaa", result[0].FullName);
            Assert.Equal("bbb", result[1].FullName);
            Assert.Equal("ccc", result[2].FullName);
        }

        [Fact]
        public void GetScmTypesReturnsHashSet()
        {
            // TODO: use different ScmTypes when more are supported
            var repos = new List<HosterRepository>();
            repos.Add(new HosterRepository("testrepo", "testrepo", "http://clone.url", ScmType.Git));
            repos.Add(new HosterRepository("testrepo2", "testrepo2", "http://clone2.url", ScmType.Git));

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
