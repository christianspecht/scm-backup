using ScmBackup.Configuration;
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

        public List<HosterRepository> GetRepositoryList(ConfigSource source, string keyProject = null )
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource.ConfigSourceIsNull);
            }

            var hoster = this.factory.Create(source.Hoster);
            return hoster.Api.GetRepositoryList(source, keyProject );
        }

        public List<HosterProject> GetProjectsList(ConfigSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource.ConfigSourceIsNull);
            }

            var hoster = this.factory.Create(source.Hoster);
            return hoster.Api.GetProjectList(source);
        }
    }
}
