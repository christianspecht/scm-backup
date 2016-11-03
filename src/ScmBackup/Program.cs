using ScmBackup.CompositionRoot;

namespace ScmBackup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bootstrapper.SetupResources();

            var container = Bootstrapper.BuildContainer();

            var context = container.GetInstance<IContext>();
            var logger = container.GetInstance<ILogger>();

            logger.Log(ErrorLevel.Info, context.AppTitle);
            
            container.GetInstance<IScmBackup>().Run();
        }
    }
}
