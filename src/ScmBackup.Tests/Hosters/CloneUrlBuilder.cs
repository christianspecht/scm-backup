namespace ScmBackup.Tests.Hosters
{
    /// <summary>
    /// Helper class to build clone URLs for various hosters
    /// Not part of IHoster and its subclasses by purpose, so we can use it in the tests without needing to create a complete IHoster instance.
    /// </summary>
    public static class CloneUrlBuilder
    {
        public static string GithubCloneUrl(string username, string reponame)
        {
            return string.Format("https://github.com/{0}/{1}", username, reponame);
        }

        public static string BitbucketCloneUrl(string username, string reponame)
        {
            return string.Format("https://bitbucket.org/{0}/{1}", username, reponame);
        }
    }
}
