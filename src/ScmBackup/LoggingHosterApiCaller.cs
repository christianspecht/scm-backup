using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System.Collections.Generic;
using System.Linq;

namespace ScmBackup
{
    internal class LoggingHosterApiCaller : IHosterApiCaller
    {
        private readonly IHosterApiCaller caller;
        private readonly ILogger logger;

        public LoggingHosterApiCaller(IHosterApiCaller caller, ILogger logger)
        {
            this.caller = caller;
            this.logger = logger;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource source)
        {
            this.logger.Log(ErrorLevel.Info, Resource.ApiGettingRepos, source.Title, source.Hoster);
            var repos = this.caller.GetRepositoryList(source);

            // #40: Bitbucket will remove all Mercurial repos on Jun 01 2020 -> show warning when the list contains at least one
            if (source.Hoster == "bitbucket")
            {
                var hgrepos = repos.Where(r => r.Scm == ScmType.Mercurial).Count();

                if (hgrepos > 0)
                {
                    this.logger.Log(ErrorLevel.Warn,Resource.BitbucketMercurialWarning, hgrepos);
                }
            }

            return repos;
        }
    }
}
