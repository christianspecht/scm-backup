using System;

namespace ScmBackup
{
    /// <summary>
    /// Gets the list of repositories for each ConfigSource
    /// </summary>
    internal class ApiCaller : IApiCaller
    {
        private readonly IHosterApiCaller apiCaller;
        private readonly IContext context;

        public ApiCaller(IHosterApiCaller apiCaller, IContext context)
        {
            if (apiCaller == null)
            {
                throw new InvalidOperationException("apiCaller is null");
            }

            if (context == null)
            {
                throw new InvalidOperationException("context is null");
            }

            this.apiCaller = apiCaller;
            this.context = context;
        }

        public ApiRepositories CallApis()
        {
            var repos = new ApiRepositories();

            foreach (var source in this.context.Config.Sources)
            {
                var tmp = this.apiCaller.GetRepositoryList(source);
                repos.AddItem(source, tmp);
            }

            return repos;
        }
    }
}
