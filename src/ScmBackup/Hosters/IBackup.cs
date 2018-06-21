namespace ScmBackup.Hosters
{
    internal interface IBackup
    {
        /// <summary>
        /// backup everything from this repo which needs to be backed up
        /// </summary>
        void MakeBackup(ConfigSource source, HosterRepository repo, string repoFolder);
    }
}
