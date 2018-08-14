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
            return EnvVar(variableName, throwException);
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

        /// <summary>
        /// Determines whether the tests are currently running on AppVeyor
        /// </summary>
        /// <remarks>
        /// Note: We could use the environment variable "CI" instead, and this would work on Travis as well
        /// (https://docs.travis-ci.com/user/environment-variables/#default-environment-variables)
        /// But we probably need to distinguish between CI providers (should we ever use more than one) because some issues (like #15) could be provider-specific
        /// </remarks>
        public static bool RunsOnAppVeyor()
        {
            // https://www.appveyor.com/docs/environment-variables/
            string v = Environment.GetEnvironmentVariable("APPVEYOR");
            if (!string.IsNullOrEmpty(v) && v.ToLower()=="true")
            {
                return true;
            }

            return false;
        }
    }
}
