using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Hosters.Gitlab
{
    internal class GitlabApiRepo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path_with_namespace { get; set; }
        public string http_url_to_repo { get; set; }
        public string visibility { get; set; }
        public bool issues_enabled { get; set; }
        public int open_issues_count { get; set; }
        public bool wiki_enabled { get; set; }
        public Dictionary<string,string> _links { get; set; }
    }
}
