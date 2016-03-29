namespace ScmBackup
{
    internal interface IHosterFactory
    {
        void Add(BaseHoster hoster);

        BaseHoster Create(string hosterName);
    }
}
