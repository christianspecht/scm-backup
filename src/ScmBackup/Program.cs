namespace ScmBackup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = Bootstrapper.BuildContainer();

            var logger = container.GetInstance<ILogger>();

            logger.Log(ErrorLevel.Info, "SCM Backup");

            container.GetInstance<IScmBackup>().Run();
        }
    }
}
