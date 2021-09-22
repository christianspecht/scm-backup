using ScmBackup.Hosters;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScmBackup.Tests
{
    public class DeletedRepoHandlerTests
    {
        FakeLogger logger;
        FakeFileSystemHelper fhelper;
        FakeContext context;

        public DeletedRepoHandlerTests()
        {
            this.logger = new FakeLogger();
            this.fhelper = new FakeFileSystemHelper();
            this.context = new FakeContext();
        }

        [Fact]
        public void DetectsDeletedDirs()
        {
            var repos = new List<HosterRepository>
            {
                new HosterRepository("repo1","repo1","url", ScmType.Git),
                new HosterRepository("repo2","repo2","url", ScmType.Git)
            };

            this.fhelper.SubDirectoryNames = new List<string> { "repo1", "repo2", "missing1", "missing2" };

            var sut = new DeletedRepoHandler(this.logger, this.fhelper, this.context);

            var result = sut.HandleDeletedRepos(repos, "dir");
            Assert.Contains("missing1", result);
            Assert.Contains("missing2", result);
        }
    }
}
