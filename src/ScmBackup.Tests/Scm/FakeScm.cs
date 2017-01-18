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

        public bool IsOnThisComputer(Config config)
        {
            if (this.IsOnThisComputerException != null)
            {
                throw this.IsOnThisComputerException;
            }

            return this.IsOnThisComputerResult;
        }
    }
}
