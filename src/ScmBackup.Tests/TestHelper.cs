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

        /// <summary>
        /// Returns the value of an environment variable. Throws an exception when the variable doesn't exist.
        /// </summary>
        public static string EnvVar(string prefix, string name)
        {
            string variableName = prefix + "_" + name;
            return EnvVar(variableName);
        }

        /// <summary>
        /// Helper to build the repository name created in ScmBackup.Hosters.HosterRepository
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="repoName"></param>
        /// <returns></returns>
        public static string BuildRepositoryName(string userName, string repoName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("userName is missing");
            }

            if (string.IsNullOrWhiteSpace(repoName))
            {
                throw new ArgumentException("repoName is missing");
            }

            return userName + "#" + repoName;
        }
    }
}
