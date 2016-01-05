using SimpleInjector;

namespace ScmBackup
{
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            var container = new Container();
            container.Verify();
        }
    }
}
