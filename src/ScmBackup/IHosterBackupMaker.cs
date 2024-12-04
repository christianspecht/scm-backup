using ScmBackup.Configuration;
using ScmBackup.Hosters;

namespace ScmBackup
{
    /// <summary>
    /// Makes a backup of one specific repository from one specific hoster
    /// </summary>
    internal interface IHosterBackupMaker
    {
        void MakeBackup(ConfigSource source, HosterRepository repo, string repoFolder);

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        void MakeBackup( ConfigSource source, HosterProject project, string projectFolder, ILogger logger );
    }
}
