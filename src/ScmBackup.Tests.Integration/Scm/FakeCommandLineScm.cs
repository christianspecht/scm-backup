using ScmBackup.Scm;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ScmBackup.Tests.Integration.Scm
{
    internal class FakeCommandLineScm : CommandLineScm, IScm
    {
        public FakeCommandLineScm()
        {
            string testAssemblyDir = DirectoryHelper.TestAssemblyDirectory();

            // some simple command with predictable result, to execute for testing
            // (probably different for each OS)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                this.FakeCommandName = Path.Combine(testAssemblyDir, @"Scm\FakeCommandLineScmTools\FakeCommandLineScm-Command-Windows.bat");
                this.FakeCommandArgs = "";
                this.FakeCommandResult = "Windows" + Environment.NewLine;

                this.FakeCommandNameNotExisting = Path.Combine(testAssemblyDir, "doesnt-exist");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                this.FakeCommandName = Path.Combine(testAssemblyDir, @"Scm/FakeCommandLineScmTools/FakeCommandLineScm-Command-Linux.sh");
                this.FakeCommandArgs = "";
                this.FakeCommandResult = "Linux";

                this.FakeCommandNameNotExisting = Path.Combine(testAssemblyDir, "doesnt-exist");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                throw new NotImplementedException("FakeCommandLineScm is not implemented for OSX");
            }

            this.context = new FakeContext();
        }

        /// <summary>
        /// command to execute
        /// </summary>
        public string FakeCommandName { get; private set; }

        /// <summary>
        /// arguments (if necessary)
        /// </summary>
        public string FakeCommandArgs { get; private set; }

        /// <summary>
        /// the result should contain this string
        /// </summary>
        public string FakeCommandResult { get; private set; }

        /// <summary>
        /// "wrong" command which doesn't exist (to test error handling)
        /// </summary>
        public string FakeCommandNameNotExisting { get; private set; }

        public IContext Context
        {
            get { return this.context; }
            set { this.context = value; }
        }

        public override string DisplayName
        {
            get { return "FakeDisplayName"; }
        }

        public override string ShortName
        {
            get { return "fake"; }
        }

        protected override string CommandName
        {
            get { return this.FakeCommandName; }
        }

        public string ExecuteCommandDirectly()
        {
            var result = this.ExecuteCommand(this.FakeCommandArgs);
            return result.Output;
        }

        public override bool IsOnThisComputer()
        {
            var result = this.ExecuteCommand(this.FakeCommandArgs);
            return result.Output.ToLower().Contains(this.FakeCommandResult.ToLower());
        }

        public override string GetVersionNumber()
        {
            throw new NotImplementedException();
        }

        public override bool DirectoryIsRepository(string directory)
        {
            throw new NotImplementedException();
        }

        public override void CreateRepository(string directory)
        {
            throw new NotImplementedException();
        }

        public override bool RemoteRepositoryExists(string remoteUrl, ScmCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public override void PullFromRemote(string remoteUrl, string directory, ScmCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public override bool RepositoryContainsCommit(string directory, string commitid)
        {
            throw new NotImplementedException();
        }
    }
}
