using ScmBackup.Scm;
using System;
using System.Runtime.InteropServices;

namespace ScmBackup.Tests.Integration.Scm
{
    internal class FakeCommandLineScm : CommandLineScm, IScm
    {
        public FakeCommandLineScm()
        {
            // some simple command with predictable result, to execute for testing
            // (probably different for each OS)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // https://en.wikipedia.org/wiki/Whoami
                this.FakeCommandName = "whoami";
                this.FakeCommandArgs = "";
                this.FakeCommandResult = Environment.MachineName;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                throw new NotImplementedException();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                throw new NotImplementedException();
            }
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
            return this.ExecuteCommand(this.FakeCommandArgs);
        }

        protected override bool IsOnThisComputer()
        {
            string result = this.ExecuteCommand(this.FakeCommandArgs);
            return result.ToLower().Contains(this.FakeCommandResult.ToLower());
        }
    }
}
