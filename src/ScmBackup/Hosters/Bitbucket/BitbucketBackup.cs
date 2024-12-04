using ScmBackup.Scm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Hosters.Bitbucket
{
    internal class BitbucketBackup : BackupBase
    {
        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public BitbucketBackup(IScmFactory scmfactory, IHosterApiCaller apiCaller)
        {
            this.scmFactory = scmfactory;
            this.apiCaller = apiCaller;
        }

        public override void BackupRepo(string subdir, ScmCredentials credentials)
        {
            this.DefaultBackup(this.repo.CloneUrl, subdir, credentials);
        }

        public override void BackupWiki(string subdir, ScmCredentials credentials)
        {
            this.DefaultBackup(this.repo.WikiUrl, subdir, credentials);
        }
    }
}
