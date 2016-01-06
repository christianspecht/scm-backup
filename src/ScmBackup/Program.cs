using SimpleInjector;

namespace ScmBackup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new Container();
            container.Register<IScmBackup, ScmBackup>();

            container.Verify();

            container.GetInstance<IScmBackup>().Run();
        }
    }
}
