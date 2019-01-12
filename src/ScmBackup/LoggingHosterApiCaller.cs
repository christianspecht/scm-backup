using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System.Collections.Generic;

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
            return this.caller.GetRepositoryList(source);
        }
    }
}
