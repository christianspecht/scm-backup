using System;

namespace ScmBackup.Tests
{
    /// <summary>
    /// helper methods for testing
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Returns the value of an environment variable. Throws an exception when the variable doesn't exist.
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public static string EnvVar(string variableName)
        {
            string result = Environment.GetEnvironmentVariable(variableName);

            if (String.IsNullOrWhiteSpace(result))
            {
                throw new ArgumentException(string.Format("Environment variable {0} not found", variableName));
            }

            return result;
        }
    }
}
