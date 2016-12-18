using ScmBackup.Hosters;
using ScmBackup.Tests.Hosters;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests
{
    public class ApiCallerTests
    {
        [Fact]
        public void ExecutesHosterApiCallerForEachConfigSource()
        {
            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            var source1 = reader.FakeConfig.Sources.First();

            var source2 = new ConfigSource();
            source2.Title = "title2";
            source2.Hoster = "fake";
            reader.FakeConfig.Sources.Add(source2);

            var config = reader.ReadConfig();


            var list1 = new List<HosterRepository>();
            list1.Add(new HosterRepository("foo1", "http://foo1", ScmType.Git));

            var list2 = new List<HosterRepository>();
            list2.Add(new HosterRepository("foo2", "http://foo2", ScmType.Git));

            var hac = new FakeHosterApiCaller();
            hac.Lists.Add(source1, list1);
            hac.Lists.Add(source2, list2);



            var sut = new ApiCaller(hac);            
            var result = sut.CallApis(config);



            Assert.Equal(2, hac.PassedConfigSources.Count);
            Assert.Equal(2, result.Dic.Count());

            var resultList1 = result.GetReposForSource(source1);
            Assert.Equal(list1, resultList1);

            var resultList2 = result.GetReposForSource(source2);
            Assert.Equal(list2, resultList2);
        }
    }
}
