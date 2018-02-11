namespace ScmBackup
{
    /// <summary>
    /// Gets the list of repositories for each ConfigSource
    /// </summary>
    internal interface IApiCaller
    {
        ApiRepositories CallApis();
    }
}
