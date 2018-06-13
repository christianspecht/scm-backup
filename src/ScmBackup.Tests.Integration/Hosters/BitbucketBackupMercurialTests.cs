using ScmBackup.Hosters.Bitbucket;
using ScmBackup.Http;
using ScmBackup.Scm;
using System.IO;
using Xunit;

namespace ScmBackup.Tests.Integration.Hosters
{
    public class BitbucketBackupMercurialTests : IBackupTests
    {
        private string prefix = "BitbucketApiTests";

        protected override void Setup()
        {
            // re-use test repo for Api tests
            var source = new ConfigSource();
            source.Hoster = "bitbucket";
            source.Type = "user";
            source.Name = TestHelper.EnvVar(prefix, "Name");
            source.AuthName = source.Name;
            source.Password = TestHelper.EnvVar(prefix, "PW");

            var config = new Config();
            config.Sources.Add(source);
            
            var context = new FakeContext();
            context.Config = config;

            var api = new BitbucketApi(new HttpRequest());
            var repoList = api.GetRepositoryList(source);
            this.repo = repoList.Find(r => r.ShortName == TestHelper.EnvVar(prefix, "Repo"));
            
            this.scm = new MercurialScm(new FileSystemHelper(), context);
            Assert.True(this.scm.IsOnThisComputer());

            var scmFactory = new FakeScmFactory();
            scmFactory.Register(ScmType.Mercurial, this.scm);
            this.sut = new BitbucketBackup(scmFactory);
        }

        protected override void AssertRepo(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
            Assert.True(scm.RepositoryContainsCommit(dir, TestHelper.EnvVar(prefix, "Commit")));
        }

        protected override void AssertWiki(string dir)
        {
            Assert.True(Directory.Exists(dir));
            Assert.True(this.scm.DirectoryIsRepository(dir));
            Assert.True(scm.RepositoryContainsCommit(dir, TestHelper.EnvVar(prefix, "WikiCommit")));
        }
    }
}
