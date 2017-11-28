using ScmBackup.Hosters;
using Xunit;

namespace ScmBackup.Tests.Hosters
{
    public class BackupBaseTests
    {
        [Fact]
        public void BackupBaseExecutesAllSubMethods()
        {
            var sut = new FakeHosterBackup();
            sut.MakeBackup(new HosterRepository("foo", "http://clone", ScmType.Git), new Config(), @"c:\foo");

            Assert.True(sut.WasExecuted);
        }

    }
}

