namespace ScmBackup.Hosters
{
    /// <summary>
    /// base interface for all hosters
    /// </summary>
    internal interface IHoster
    {
        string Name { get; }

        IConfigSourceValidator Validator { get; }
    }
}
