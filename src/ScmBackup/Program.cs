using SimpleInjector;

namespace ScmBackup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new Container();
            container.Register<IScmBackup, ScmBackup>();

            container.RegisterCollection<ILogger>(new[] {
                typeof(ConsoleLogger)
            });
            container.Register<ILogger, CompositeLogger>();

            container.Verify();

            container.GetInstance<IScmBackup>().Run();
        }
    }
}
