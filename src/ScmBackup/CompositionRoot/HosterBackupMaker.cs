using ScmBackup.Hosters;
using System;

namespace ScmBackup.CompositionRoot
{
    /// <summary>
    /// Makes a backup of one specific repository from one specific hoster
    /// </summary>
    internal class HosterBackupMaker : IHosterBackupMaker
    {
        private readonly IHosterFactory factory;

        public HosterBackupMaker(IHosterFactory factory)
        {
            this.factory = factory;
        }

        public void MakeBackup(ConfigSource source, HosterRepository repo, Config config, string repoFolder)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource.ConfigSourceIsNull);
            }

            if (config == null)
            {
                throw new ArgumentNullException(Resource.ConfigIsNull);
            }

            var hoster = factory.Create(source.Hoster);
            hoster.Backup.MakeBackup(repo, config, repoFolder);
        }
    }
}
