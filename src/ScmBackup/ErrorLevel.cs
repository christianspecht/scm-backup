namespace ScmBackup
{
    /// <summary>
    /// Available error levels
    /// </summary>
    internal enum ErrorLevel
    {
        Debug = 0,
        Info = 1,
        Warn = 2,
        Error = 3
    }

    internal static class ErrorLevelExtensions
    {
        public static string LevelName(this ErrorLevel level)
        {
            return level.ToString("f"); // https://stackoverflow.com/a/32726578/6884
        }
    }
}
