using ScmBackup.Configuration;

namespace ScmBackup.Hosters
{
    internal interface IBackup
    {
        /// <summary>
        /// backup everything from this repo which needs to be backed up
        /// </summary>
        void MakeBackup(ConfigSource source, HosterRepository repo, string repoFolder);

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        void MakeBackup(ConfigSource source, HosterProject project, string repoFolder, ILogger logger );
    }
}
