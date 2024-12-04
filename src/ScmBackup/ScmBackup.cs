using Org.BouncyCastle.Math.EC.Rfc7748;
using ScmBackup.Configuration;
using ScmBackup.Scm;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace ScmBackup
{
    /// <summary>
    /// main program execution
    /// </summary>
    internal class ScmBackup : IScmBackup
    {
        private readonly IApiCaller apiCaller;
        private readonly IScmValidator validator;
        private readonly IBackupMaker backupMaker;
        private readonly IConfigBackupMaker configBackupMaker;
        private readonly IDeletedRepoHandler deletedHandler;

        private readonly IContext context;
        private readonly ILogger logger;

        public ScmBackup(IApiCaller apiCaller, IScmValidator validator, IBackupMaker backupMaker, IConfigBackupMaker configBackupMaker, IDeletedRepoHandler deletedHandler, IContext context, ILogger logger)
        {
            this.apiCaller = apiCaller;
            this.validator = validator;
            this.backupMaker = backupMaker;
            this.configBackupMaker = configBackupMaker;
            this.deletedHandler = deletedHandler;
            this.context = context;
            this.logger = logger;
        }

        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public bool Run()
        {
            this.configBackupMaker.BackupConfigs();

            /*
                * Add by ISC. Gicel Cordoba Pech. 
                Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
                Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
            */
            if ( this.context.Config.Options.Backup.BackupByProject ) {

                this.logger.Log( ErrorLevel.Info, Resource.BackupTypeByProjects, "Backup of repositories through projects..." );

                this.logger.Log( ErrorLevel.Info, Resource.DadLovesThem, "1- Mis niñas Sofi y Pau, Las Amoooooo mucho...!!!!" );

                //this.logger.Log( ErrorLevel.Info, Resource.Vacation, "Mis niñas Sofi y Pau, Nos vamos de vaciones a la riviera maya en septiembre y en diciembre a México, si DIOS lo permite...!!!!" );

                var projects = this.apiCaller.CallApisProjects();
    
                foreach ( var source in projects.GetSources() )
                {
                    var sourceProjects = projects.GetProjectsForSource( source );
                    string sourceFolder = this.backupMaker.Backup( source, sourceProjects );
                    this.deletedHandler.HandleDeleteProjects( sourceProjects, sourceFolder );
                }

            }
            else {

                this.logger.Log( ErrorLevel.Info, Resource.BackupTypeByRepository, "Backup by repository..." );

                var repos = this.apiCaller.CallApis();

                if ( !this.validator.ValidateScms(repos.GetScmTypes()) )
                {
                    throw new InvalidOperationException(Resource.ScmValidatorError);
                }
                
                foreach (var source in repos.GetSources())
                {
                    var sourceRepos = repos.GetReposForSource(source);
                    string sourceFolder = this.backupMaker.Backup(source, sourceRepos);
                    this.deletedHandler.HandleDeletedRepos(sourceRepos, sourceFolder);
                }

            }

            return true;
        }
    }
}
