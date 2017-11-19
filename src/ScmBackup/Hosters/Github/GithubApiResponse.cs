namespace ScmBackup.Hosters.Github
{
    public class GithubApiResponse
    {
        public string full_name { get; set; }
        public string clone_url { get; set; }
        public bool has_wiki { get; set; }
        public bool has_issues { get; set; }
        public string issues_url { get; set; }

    }
}
