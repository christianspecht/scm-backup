namespace ScmBackup.Hosters
{
    /// <summary>
    /// base class for all hosters
    /// </summary>
    internal abstract class BaseHoster
    {
        public abstract string Name { get; }

        public IConfigSourceValidator Validator { get; protected set; }
    }
}
