using ScmBackup.Http;
using System;
using System.Collections.Generic;

namespace ScmBackup.Hosters.Bitbucket
{
    internal class BitbucketApi : IHosterApi
    {
        public HttpResult LastResult
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public List<HosterRepository> GetRepositoryList(ConfigSource config)
        {
            throw new NotImplementedException();
        }
    }
}
