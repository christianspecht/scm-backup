namespace ScmBackup.Hosters
{
    /// <summary>
    /// base interface for all hosters
    /// </summary>
    internal interface IHoster
    {
        IConfigSourceValidator Validator { get; }
    }
}
