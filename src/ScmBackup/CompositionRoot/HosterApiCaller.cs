using ScmBackup.Hosters;
using System;
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
            if (config == null)
            {
                throw new ArgumentNullException(Resource.ConfigSourceIsNull);
            }

            var hoster = this.factory.Create(config.Hoster);
            return hoster.Api.GetRepositoryList(config);
        }
    }
}
