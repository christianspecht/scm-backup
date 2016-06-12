using ScmBackup.Hosters;
    
namespace ScmBackup
{
    internal interface IHosterFactory
    {
        void Add(IHoster hoster);

        IHoster Create(string hosterName);
    }
}
