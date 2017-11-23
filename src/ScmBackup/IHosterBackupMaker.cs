using ScmBackup.Hosters;

namespace ScmBackup
{
    /// <summary>
    /// Makes a backup of one specific repository from one specific hoster
    /// </summary>
    internal interface IHosterBackupMaker
    {
        void MakeBackup(ConfigSource source, HosterRepository repo, Config config, string repoFolder);
    }
}
