using ScmBackup.Configuration;
using ScmBackup.Hosters;
using System.Collections.Generic;
using System.Linq;

namespace ScmBackup
{
    internal class LoggingHosterApiCaller : IHosterApiCaller
    {
        private readonly IHosterApiCaller caller;
        private readonly ILogger logger;

        public LoggingHosterApiCaller(IHosterApiCaller caller, ILogger logger)
        {
            this.caller = caller;
            this.logger = logger;
        }

        /*
            * Modified by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterRepository> GetRepositoryList(ConfigSource source, string keyProject = null)
        {
            this.logger.Log( ErrorLevel.Info, Resource.DadLovesThem, "2- Mis niñas Sofi y Pau, Las Amoooooo mucho...!!!!" );
            this.logger.Log(ErrorLevel.Info, Resource.ApiGettingRepos, source.Title, source.Hoster);
            

            return this.caller.GetRepositoryList(source, keyProject);

        }


        /*
            * Add by ISC. Gicel Cordoba Pech. 
            Chicxulub puerto Progreso, Mérida Yucatán . As of June 18, 2024
            Company: Fundación Rafael Dondé. position: INGENIERO CD CI DEVOPS
        */
        public List<HosterProject> GetProjectsList( ConfigSource source ) {

            this.logger.Log( ErrorLevel.Info, Resource.Vacation, "Mis niñas Sofi y Pau, Nos vamos de vaciones a la riviera maya en septiembre y en diciembre a México, si DIOS lo permite...!!!!" );
            this.logger.Log(ErrorLevel.Info, Resource.ApiGettingProjects, source.Title, source.Hoster);
            

            return this.caller.GetProjectsList( source );
            
        }


    }
}
