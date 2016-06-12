using System;

namespace ScmBackup.Hosters
{
    /// <summary>
    /// BaseHoster implementation for GitHub
    /// </summary>
    internal class GithubHoster : IHoster
    {
        public GithubHoster()
        {
            this.Name = "github";
            this.Validator = new GithubConfigSourceValidator();
        }

        public string Name { get; private set; }

        public IConfigSourceValidator Validator { get; private set; }
    }
}
