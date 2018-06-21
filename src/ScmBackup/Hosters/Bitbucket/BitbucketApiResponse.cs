using System.Collections.Generic;

namespace ScmBackup.Hosters.Bitbucket
{
    internal class BitbucketApiResponse
    {
        public List<Repo> values { get; set; }
        public string next { get; set; }

        internal class Repo
        {
            public string scm { get; set; }
            public string name { get; set; }
            public string full_name { get; set; }
            public bool has_wiki { get; set; }
            public bool has_issues { get; set; }
            public bool is_private { get; set; }
            public Links links { get; set; }

            internal class Links
            {
                public List<Clone> clone { get; set; }

                internal class Clone
                {
                    public string href { get; set; }
                    public string name { get; set; }
                }
            }
        }
    }
}
