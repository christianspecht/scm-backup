using System;

namespace ScmBackup
{
    /// <summary>
    /// Gets the list of repositories for each ConfigSource
    /// </summary>
    internal class ApiCaller : IApiCaller
    {
        private readonly IHosterApiCaller apiCaller;

        public ApiCaller(IHosterApiCaller apiCaller)
        {
            if (apiCaller == null)
            {
                throw new InvalidOperationException("apiCaller is null");
            }

            this.apiCaller = apiCaller;
        }

        public ApiRepositories CallApis(Config config)
        {
            if (config == null)
            {
                throw new InvalidOperationException("config is null");
            }

            var repos = new ApiRepositories();

            foreach (var source in config.Sources)
            {
                var tmp = this.apiCaller.GetRepositoryList(source);
                repos.AddItem(source, tmp);
            }

            return repos;
        }
    }
}
