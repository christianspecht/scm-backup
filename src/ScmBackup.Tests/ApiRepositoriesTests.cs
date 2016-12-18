using ScmBackup.Hosters;
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
    }
}
