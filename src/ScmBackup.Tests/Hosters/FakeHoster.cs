using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup.Tests.Hosters
{
    internal class FakeHoster : IHoster
    {
        public FakeHoster()
        {
            this.Validator = new FakeConfigSourceValidator();
            this.FakeValidator.Result = new ValidationResult();

            this.Api = new FakeHosterApi();
            this.FakeApi.RepoList = new List<HosterRepository>();

            this.Backup = new FakeHosterBackup();
        }

        /// <summary>
        /// easier access (without casting) to the fake validator
        /// </summary>
        public FakeConfigSourceValidator FakeValidator
        {
            get { return (FakeConfigSourceValidator)this.Validator; }
        }

        /// <summary>
        /// easier access (without casting) to the fake API
        /// </summary>
        public FakeHosterApi FakeApi
        {
            get { return (FakeHosterApi)this.Api; }
        }

        /// <summary>
        /// easier access (without casting) to the fake backupper
        /// </summary>
        public FakeHosterBackup FakeBackup
        {
            get { return (FakeHosterBackup)this.Backup; }
        }

        public IConfigSourceValidator Validator { get; private set; }
        public IHosterApi Api { get; private set; }
        public IBackup Backup { get; private set; }
    }
}
