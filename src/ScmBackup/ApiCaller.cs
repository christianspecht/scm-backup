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
            this.apiCaller = apiCaller;
        }

        public ApiRepositories CallApis(Config config)
        {
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
