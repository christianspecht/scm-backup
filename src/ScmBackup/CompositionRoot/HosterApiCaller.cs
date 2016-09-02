using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup.CompositionRoot
{
    internal class HosterApiCaller : IHosterApiCaller
    {
        private readonly IHosterFactory factory;

        public HosterApiCaller(IHosterFactory factory)
        {
            this.factory = factory;
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource config)
        {
            var hoster = this.factory.Create(config.Hoster);
            return hoster.Api.GetRepositoryList(config);
        }
    }
}
