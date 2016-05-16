namespace ScmBackup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bootstrapper.SetupResources();

            var container = Bootstrapper.BuildContainer();

            var logger = container.GetInstance<ILogger>();

            logger.Log(ErrorLevel.Info, Resource.GetString("AppTitle"));

            container.GetInstance<IScmBackup>().Run();
        }
    }
}
