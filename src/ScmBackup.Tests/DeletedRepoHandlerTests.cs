using ScmBackup.Hosters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ScmBackup.Tests
{
    public class DeletedRepoHandlerTests
    {
        FakeLogger logger;
        FakeFileSystemHelper fhelper;
        FakeContext context;
        List<HosterRepository> repos;

        public DeletedRepoHandlerTests()
        {
            this.logger = new FakeLogger();
            this.fhelper = new FakeFileSystemHelper();
            this.context = new FakeContext();

            this.repos = new List<HosterRepository>
            {
                new HosterRepository("repo1","repo1","url", ScmType.Git),
                new HosterRepository("repo2","repo2","url", ScmType.Git)
            };

            this.fhelper.SubDirectoryNames = new List<string> { "repo1", "repo2", "missing1", "missing2" };
        }

        [Fact]
        public void DetectsDeletedDirs()
        {
            var sut = new DeletedRepoHandler(this.logger, this.fhelper, this.context);
            var result = sut.HandleDeletedRepos(repos, "dir");

            Assert.Contains("missing1", result);
            Assert.Contains("missing2", result);
        }

        [Fact]
        public void RemovesDeletedDirs()
        {
            this.context.Config.Options.Backup.RemoveDeletedRepos = true;

            var sut = new DeletedRepoHandler(this.logger, this.fhelper, this.context);
            var result = sut.HandleDeletedRepos(repos, "dir");

            var missing1 = fhelper.DeletedDirectories.Where(x => x.Contains("missing1")).FirstOrDefault();
            Assert.NotNull(missing1);

            var missing2 = fhelper.DeletedDirectories.Where(x => x.Contains("missing2")).FirstOrDefault();
            Assert.NotNull(missing2);
        }
    }
}
