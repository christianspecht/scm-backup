using ScmBackup.Hosters;
using SimpleInjector;

namespace ScmBackup
{
    public class Program
    {
        public static void Main(string[] args)
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

            var logger = container.GetInstance<ILogger>();

            logger.Log(ErrorLevel.Info, "SCM Backup");

            container.GetInstance<IScmBackup>().Run();
        }
    }
}
