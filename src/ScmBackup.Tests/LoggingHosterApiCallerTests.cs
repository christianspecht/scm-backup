using ScmBackup.Configuration;
using ScmBackup.Hosters;
using ScmBackup.Tests.Hosters;
using System.Collections.Generic;
using Xunit;

namespace ScmBackup.Tests
{
    public class LoggingHosterApiCallerTests
    {
        ConfigSource source;
        LoggingHosterApiCaller sut;
        FakeLogger logger;
        List<HosterRepository> repos;

        public LoggingHosterApiCallerTests()
        {
            this.source = new ConfigSource { Title = "foo" };

            this.repos = new List<HosterRepository>();
            this.repos.Add(new HosterRepository("foo.bar", "bar", "http://clone", ScmType.Git));

            var caller = new FakeHosterApiCaller();
            caller.Lists.Add(this.source, repos);

            this.logger = new FakeLogger();

            this.sut = new LoggingHosterApiCaller(caller, logger);
        }

        [Fact]
        public void LogsInfoMessage()
        {
            this.sut.GetRepositoryList(this.source);

            Assert.True(this.logger.LoggedSomething);
            Assert.Equal(ErrorLevel.Info, this.logger.LastErrorLevel);
        }

        [Fact]
        public void LogsWarningForMercurialOnBitbucket()
        {
            // #40: Bitbucket will remove all Mercurial repos on Jun 01 2020 -> show warning when the list contains at least one
            this.source.Hoster = "bitbucket";
            this.repos.Add(new HosterRepository("foo.hg", "hg", "http://clone", ScmType.Mercurial));

            this.sut.GetRepositoryList(this.source);

            Assert.True(this.logger.LoggedSomething);
            Assert.Equal(ErrorLevel.Warn, this.logger.LastErrorLevel);
        }
    }
}
