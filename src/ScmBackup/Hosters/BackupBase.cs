using ScmBackup.Configuration;
using ScmBackup.Scm;
using System;
using System.IO;
using System.Collections.Generic;


/*using ScmBackup;*/
using ScmBackup.Http;

namespace ScmBackup.Hosters
{
    internal abstract class BackupBase : IBackup, IHosterApiCaller
    {
        public readonly string SubDirRepo = "repo";
        public readonly string SubDirWiki = "wiki";
        public readonly string SubDirIssues = "issues";
        protected IScm scm;

        protected HosterRepository repo;
        
        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        private ILogger logger;

        protected HosterProject project;

        // this MUST be filled in the child classes' constructor
        public IScmFactory scmFactory;

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public IHosterApiCaller apiCaller;

        /**/
        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterRepository> GetRepositoryList(ConfigSource source, string keyProject = null)
        {
            var list = this.apiCaller.GetRepositoryList(source, keyProject);

            return list;
        }

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterProject> GetProjectsList( ConfigSource source )
        {

            return this.apiCaller.GetProjectsList(source);
            
        }
        /**/

        public void MakeBackup(ConfigSource source, HosterRepository repo, string repoFolder)
        {
            if (this.scmFactory == null)
            {
                throw new InvalidOperationException(string.Format(Resource.BackupBase_IScmfactoryIsMissing, source.Hoster));
            }

            ScmCredentials credentials = null;
            if (repo.IsPrivate)
            {
                credentials = new ScmCredentials(source.AuthName, source.Password);
            }

            this.repo = repo;

            string subdir = Path.Combine(repoFolder, this.SubDirRepo);
            this.BackupRepo(subdir, credentials);

            if (this.repo.HasWiki)
            {
                subdir = Path.Combine(repoFolder, this.SubDirWiki);
                this.BackupWiki(subdir, credentials);
            }

            if (this.repo.HasIssues)
            {
                subdir = Path.Combine(repoFolder, this.SubDirIssues);
                this.BackupIssues(subdir, credentials);
            }
        }

        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public void MakeBackup(ConfigSource source, HosterProject project, string projectFolder, ILogger logger)
        {
            if (this.scmFactory == null)
            {
                throw new InvalidOperationException(string.Format(Resource.BackupBase_IScmfactoryIsMissing, source.Hoster));
            }

            ScmCredentials credentials = null;
            /*if (project.IsPrivate)
            {
                credentials = new ScmCredentials(source.AuthName, source.Password);
            }*/

            credentials = new ScmCredentials(source.AuthName, source.Password);

            this.project = project;
            this.logger = logger;

            //string subdir = Path.Combine(projectFolder, this.SubDirRepo);
            //this.BackupRepo(subdir, credentials); //Aqui posible poner la llamada, por cada project, el backup del rep

            var url = new UrlHelper();

            /*var api = new BitbucketApi( new HttpRequest() );
            //var repoList = api.GetRepositoryList( source, project.FullName, project.Key );
            var repoList = api.GetRepositoryList( source, project.Key );*/

            var repoList = this.apiCaller.GetRepositoryList( source, project.Key );

            foreach( var repo in repoList )
            {
                this.repo = repo;

                string subdir = Path.Combine(projectFolder, this.repo.FullName + Path.DirectorySeparatorChar + this.SubDirRepo);

                this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Repo, repo.Scm.ToString(), url.RemoveCredentialsFromUrl(repo.CloneUrl));
                
                this.BackupRepo(subdir, credentials);
            }

        }

        public void InitScm()
        {
            if (this.scm == null)
            {
                this.scm = this.scmFactory.Create(this.repo.Scm);
                if (!this.scm.IsOnThisComputer())
                {
                    throw new InvalidOperationException(string.Format(Resource.ScmNotOnThisComputer, this.repo.Scm.ToString()));
                }
            }

        }

        // this MUST be implemented in the child classes
        public abstract void BackupRepo(string subdir, ScmCredentials credentials);

        // these can be implemented in the child classes IF the given hoster has issues, a wiki...
        public virtual void BackupWiki(string subdir, ScmCredentials credentials) { }
        public virtual void BackupIssues(string subdir, ScmCredentials credentials) { }

        /// <summary>
        /// default implementation for backups if nothing special is needed
        /// </summary>
        protected void DefaultBackup(string cloneurl, string subdir, ScmCredentials credentials)
        {
            InitScm();
            scm.PullFromRemote(cloneurl, subdir, credentials);

            if (!scm.DirectoryIsRepository(subdir))
            {
                throw new InvalidOperationException(Resource.DirectoryNoRepo);
            }
        }
    }
}
