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

        public void MakeBackup(ConfigSource source, HosterRepository repo,string repoFolder)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource.ConfigSourceIsNull);
            }

            var hoster = factory.Create(source.Hoster);
            hoster.Backup.MakeBackup(repo, repoFolder);
        }
    }
}
