using ScmBackup.Hosters;
using System.Collections.Generic;

namespace ScmBackup
{
    internal interface IBackupMaker
    {
        void Backup(Config config, ConfigSource source, IEnumerable<HosterRepository> repos);
    }
}
