using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Scm
{
    [Scm(Type = ScmType.Git)]
    internal class LibgitScm : IScm
    {
        public string ShortName => "git";

        public string DisplayName => "Git";

        public void CreateRepository(string directory)
        {
            throw new NotImplementedException();
        }

        public bool DirectoryIsRepository(string directory)
        {
            throw new NotImplementedException();
        }

        public string GetVersionNumber()
        {
            throw new NotImplementedException();
        }

        public bool IsOnThisComputer()
        {
            throw new NotImplementedException();
        }

        public void PullFromRemote(string remoteUrl, string directory)
        {
            throw new NotImplementedException();
        }

        public void PullFromRemote(string remoteUrl, string directory, ScmCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool RemoteRepositoryExists(string remoteUrl)
        {
            throw new NotImplementedException();
        }

        public bool RemoteRepositoryExists(string remoteUrl, ScmCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool RepositoryContainsCommit(string directory, string commitid)
        {
            throw new NotImplementedException();
        }
    }
}
