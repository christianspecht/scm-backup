using ScmBackup.Configuration;
using ScmBackup.Scm;
using System;

namespace ScmBackup.Tests.Scm
{
    internal class FakeScm : IScm
    {
        /// <summary>
        /// Value returned by IsOnThisComputer()
        /// </summary>
        public bool IsOnThisComputerResult { get; set; }

        /// <summary>
        /// Exception to be thrown by IsOnThisComputer
        /// </summary>
        public Exception IsOnThisComputerException { get; set; }

        public string ShortName
        {
            get { return "fake"; }
        }

        public string DisplayName
        {
            get { return "Fake"; }
        }

        public bool IsOnThisComputer()
        {
            return this.IsOnThisComputer(null);
        }

        public bool IsOnThisComputer(Config config)
        {
            if (this.IsOnThisComputerException != null)
            {
                throw this.IsOnThisComputerException;
            }

            return this.IsOnThisComputerResult;
        }

        public string GetVersionNumber()
        {
            return "fake";
        }

        public bool DirectoryIsRepository(string directory)
        {
            throw new NotImplementedException();
        }

        public void CreateRepository(string directory)
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

        public void PullFromRemote(string remoteUrl, string directory)
        {
            throw new NotImplementedException();
        }

        public void PullFromRemote(string remoteUrl, string directory, ScmCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool RepositoryContainsCommit(string directory, string commitid)
        {
            throw new NotImplementedException();
        }
    }
}
