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

            container.RegisterCollection<ILogger>(new[] {
                typeof(ConsoleLogger)
            });
            container.Register<ILogger, CompositeLogger>();

            container.Register<IConfigReader, ConfigReader>();
            container.RegisterDecorator<IConfigReader, ValidatingConfigReader>();

            container.Verify();

            var logger = container.GetInstance<ILogger>();

            logger.Log(LogLevel.Info, "SCM Backup");

            container.GetInstance<IScmBackup>().Run();
        }
    }
}
