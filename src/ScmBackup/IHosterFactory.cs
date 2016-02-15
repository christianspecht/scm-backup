namespace ScmBackup
{
    public interface IHosterFactory
    {
        void Add(IHoster hoster);

        IHoster Create(string hosterName);
    }
}
