using ScmBackup.CompositionRoot;

namespace ScmBackup
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var container = Bootstrapper.BuildContainer();            
            var success = container.GetInstance<IScmBackup>().Run();
            return success ? 0 : 1;
        }
    }
}
