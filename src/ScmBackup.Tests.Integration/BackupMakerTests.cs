using ScmBackup.Hosters;
using ScmBackup.Scm;
using ScmBackup.Tests.Integration.Scm;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class BackupMakerTests
    {
        [Fact]
        public void BackupMakerRuns()
        {
            // We're using Git for testing, and we're using the same public repo as in the integration tests for Git itself:
            var scmType = ScmType.Git;
            IScm scm = new GitScm();
            var tests = new GitScmTests();
            string repoUrl = tests.PublicRepoUrl;
            string commitId = tests.PublicRepoExistingCommitId;


            // parameters for the class instance
            var logger = new FakeLogger();
            var fileHelper = new FileSystemHelper();

            var scmFactory = new FakeScmFactory();
            scmFactory.Add(scmType, scm);


            // parameters for the method
            var config = new Config();
            config.LocalFolder = DirectoryHelper.CreateTempDirectory("backupmaker");

            var source = new ConfigSource();
            source.Title = "foo";

            string repoName = "repo";
            var repo = new HosterRepository(repoName, repoUrl, scmType);
            var repos = new List<HosterRepository>();
            repos.Add(repo);


            // the actual test
            var sut = new BackupMaker(logger, scmFactory, fileHelper);
            sut.Backup(config, source, repos);

            // the resulting repo should be here:
            string finalDir = Path.Combine(config.LocalFolder, source.Title, repoName);

            Assert.True(Directory.Exists(finalDir));
            Assert.True(scm.DirectoryIsRepository(finalDir));
            Assert.True(scm.RepositoryContainsCommit(finalDir, commitId));
        }
    }
}
