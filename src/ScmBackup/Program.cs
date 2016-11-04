using ScmBackup.CompositionRoot;

namespace ScmBackup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bootstrapper.SetupResources();

            var container = Bootstrapper.BuildContainer();            
            container.GetInstance<IScmBackup>().Run();
        }
    }
}
