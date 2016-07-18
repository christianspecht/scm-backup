using ScmBackup.Hosters;

namespace ScmBackup.CompositionRoot
{
    internal interface IHosterFactory
    {
        IHoster Create(string hosterName);
    }
}
