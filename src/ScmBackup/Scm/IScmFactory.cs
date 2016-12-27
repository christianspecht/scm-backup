namespace ScmBackup.Scm
{
    /// <summary>
    /// factory to create IScm instances
    /// </summary>
    internal interface IScmFactory
    {
        IScm Create(ScmType type);
    }
}
