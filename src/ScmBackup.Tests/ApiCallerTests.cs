using ScmBackup.Hosters;
using ScmBackup.Tests.Hosters;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests
{
    public class ApiCallerTests
    {
        private ConfigSource source1;
        private ConfigSource source2;
        private FakeContext context;

        private List<HosterRepository> list1;
        private List<HosterRepository> list2;
        private FakeHosterApiCaller hac;

        public ApiCallerTests()
        {
            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            source1 = reader.FakeConfig.Sources.First();

            source2 = new ConfigSource();
            source2.Title = "title2";
            source2.Hoster = "fake";
            reader.FakeConfig.Sources.Add(source2);

            this.context = new FakeContext();
            this.context.Config = reader.ReadConfig();

            list1 = new List<HosterRepository>();
            list1.Add(new HosterRepository("foo1", "foo1", "http://foo1", ScmType.Git));

            list2 = new List<HosterRepository>();
            list2.Add(new HosterRepository("foo2", "foo2", "http://foo2", ScmType.Git));

            hac = new FakeHosterApiCaller();
            hac.Lists.Add(source1, list1);
            hac.Lists.Add(source2, list2);
        }

        [Fact]
        public void ExecutesHosterApiCallerForEachConfigSource()
        {
            var sut = new ApiCaller(hac, context);
            var result = sut.CallApis();

            Assert.Equal(2, hac.PassedConfigSources.Count);
            Assert.Equal(2, result.Dic.Count());

            var resultList1 = result.GetReposForSource(source1);
            Assert.Equal(list1, resultList1);

            var resultList2 = result.GetReposForSource(source2);
            Assert.Equal(list2, resultList2);
        }

        [Fact]
        public void ThrowsWhenNoHosterApiCallerIsPassed()
        {
            Assert.ThrowsAny<Exception>(() => new ApiCaller(null, context));
        }

        [Fact]
        public void ThrowsWhenNoContextIsPassed()
        {
            Assert.ThrowsAny<Exception>(() => new ApiCaller(hac, null));
        }
    }
}
