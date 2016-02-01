namespace ScmBackup
{
    /// <summary>
    /// Reads the configuration values and returns an instance of the Config class
    /// </summary>
    internal interface IConfigReader
    {
        Config ReadConfig();
    }
}
