namespace ScmBackup.Hosters
{
    /// <summary>
    /// Data to access one single repository
    /// </summary>
    internal class HosterRepository
    {
        public HosterRepository(string fullName, string shortName, string cloneUrl, ScmType scm )
        {
            SetFullName(fullName);
            this.ShortName = shortName;
            this.CloneUrl = cloneUrl;
            this.Scm = scm;
            //this.projectKey = projectKey;
        }

        //public HosterRepository(string fullName, string shortName, string cloneUrl, ScmType scm, bool haswiki, string wikiurl, bool hasissues, string issueurl, string projectFullName = null, string projectKey = null)
        public HosterRepository(string fullName, string shortName, string cloneUrl, ScmType scm, bool haswiki, string wikiurl, bool hasissues, string issueurl)
        {
            SetFullName(fullName);
            this.ShortName = shortName;
            this.CloneUrl = cloneUrl;
            this.Scm = scm;
            SetWiki(haswiki, wikiurl);
            SetIssues(hasissues, issueurl);
            //this.projectKey = projectKey; 
        }

        /// <summary>
        /// Full name of the repository (e.g. "username/reponame")
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// "short name" of the repository (e.g. "reponame")
        /// </summary>
        public string ShortName { get; private set; }

        /// <summary>
        /// URL to clone the repository
        /// </summary>
        public string CloneUrl { get; private set; }

        /// <summary>
        /// The SCM of the repository
        /// </summary>
        public ScmType Scm { get; private set; }

        public string projectKey { get; set; }
        /// <summary>
        /// Does the repo have a wiki?
        /// </summary>
        public bool HasWiki { get; private set; }

        /// <summary>
        /// URL to backup the wiki, if one exists)
        /// </summary>
        public string WikiUrl { get; private set; }

        /// <summary>
        /// Does the repo have issues?
        /// </summary>
        public bool HasIssues { get; private set; }

        /// <summary>
        /// URL to backup the issues
        /// </summary>
        public string IssueUrl { get; private set; }

        /// <summary>
        /// the repo is private
        /// </summary>
        public bool IsPrivate { get; private set; }

        public void SetFullName(string name)
        {
            this.FullName = name.Replace('/', '#');
        }

        public void SetWiki(bool haswiki, string wikiurl)
        {
            this.HasWiki = haswiki;
            this.WikiUrl = wikiurl;
        }

        public void SetIssues(bool hasissues, string issueurl)
        {
            this.HasIssues = hasissues;
            this.IssueUrl = issueurl;
        }

        public void SetPrivate(bool isPrivate)
        {
            this.IsPrivate = isPrivate;
        }
    }
}
