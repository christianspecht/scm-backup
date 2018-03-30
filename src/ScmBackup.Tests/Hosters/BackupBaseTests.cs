using ScmBackup.Hosters;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class BackupBaseTests
    {
        [Fact]
        public void BackupBaseExecutesAllSubMethods()
        {
            var repo = new HosterRepository("foo", "foo", "http://clone", ScmType.Git);
            repo.SetWiki(true, "http://wiki");
            repo.SetIssues(true, "http://issues");

            var sut = new FakeHosterBackup();
            sut.MakeBackup(repo, @"c:\foo");

            Assert.True(sut.WasExecuted);
        }

    }
}

