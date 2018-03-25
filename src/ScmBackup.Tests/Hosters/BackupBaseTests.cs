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
            sut.MakeBackup(new HosterRepository("foo", "foo", "http://clone", ScmType.Git), @"c:\foo");

            Assert.True(sut.WasExecuted);
        }

    }
}

