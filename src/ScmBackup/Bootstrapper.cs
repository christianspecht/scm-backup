using ScmBackup.Hosters;
using SimpleInjector;

namespace ScmBackup
{
    public class Bootstrapper
    {
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

            container.Verify();

            return container;
        }
    }
}
