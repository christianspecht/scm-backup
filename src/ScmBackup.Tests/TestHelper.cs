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
        public static string EnvVar(string variableName)
        {
            return EnvVar(variableName, true);
        }

        /// <summary>
        /// Returns the value of an environment variable. Optional: Throws an exception when the variable doesn't exist.
        /// </summary>
        public static string EnvVar(string variableName, bool throwException)
        {
            string result = Environment.GetEnvironmentVariable(variableName);

            if (throwException)
            {
                if (String.IsNullOrWhiteSpace(result))
                {
                    throw new ArgumentException(string.Format("Environment variable {0} not found", variableName));
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the value of an environment variable. Throws an exception when the variable doesn't exist.
        /// </summary>
        public static string EnvVar(string prefix, string name)
        {
            return EnvVar(prefix, name, true);
        }

        /// <summary>
        /// Returns the value of an environment variable. Optional: Throws an exception when the variable doesn't exist.
        /// </summary>
        public static string EnvVar(string prefix, string name, bool throwException)
        {
            string variableName = prefix + "_" + name;
            return EnvVar(variableName);
        }

        /// <summary>
        /// Helper to build the repository name created in ScmBackup.Hosters.HosterRepository
        /// </summary>
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
