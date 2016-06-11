using ScmBackup.Hosters;
using ScmBackup.Http;
using ScmBackup.Resources;
using SimpleInjector;
using System.Globalization;

namespace ScmBackup
{
    public class Bootstrapper
    {
        public static void SetupResources()
        {
            // TODO: determine current culture
            var culture = new CultureInfo("en-US");

            Resource.Initialize(new ResourceProvider(), culture);
        }

        /// <summary>
        /// Registers IoC dependencies and returns the initialized container
        /// </summary>
        /// <returns></returns>
        public static Container BuildContainer()
        {
            var container = new Container();
            container.Register<IScmBackup, ScmBackup>();
            container.RegisterDecorator<IScmBackup, ErrorHandlingScmBackup>();

            container.RegisterCollection<ILogger>(new ConsoleLogger(), new NLogLogger());
            container.Register<ILogger, CompositeLogger>(Lifestyle.Singleton);

            var hosterFactory = new HosterFactory();
            hosterFactory.Add(new GithubHoster());
            container.RegisterSingleton<IHosterFactory>(hosterFactory);

            container.Register<IConfigReader, ConfigReader>();
            container.RegisterDecorator<IConfigReader, ValidatingConfigReader>();

            container.Register<IHttpRequest, HttpRequest>();

            container.Verify();

            return container;
        }
    }
}
