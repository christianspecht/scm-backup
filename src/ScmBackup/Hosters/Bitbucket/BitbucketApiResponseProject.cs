/*
    * Add by ISC. Gicel Cordoba Pech. 
    Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
    Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
*/

using System.Collections.Generic;

namespace ScmBackup.Hosters.Bitbucket
{
    internal class BitbucketApiResponseProject
    {  
        public List<Project> values { get; set; }
        public string next { get; set; }

        /*
         * Add by ISC. Gicel Cordoba Pech. 
           Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
           Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        internal class Project
        {
            public string scm { get; set; }
            public string name { get; set; }
            public string key { get; set; }
            public bool is_private { get; set; }
        }

    }

}
