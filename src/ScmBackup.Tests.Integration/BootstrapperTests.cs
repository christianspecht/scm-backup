using System;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class BootstrapperTests
    {
        [Fact]
        public void DependenciesWereResolved()
        {
            // Just initialize the container like on startup.
            // If a dependency could not be resolved, it will throw an exception.
            bool error = false;

            try
            {
                var container = Bootstrapper.BuildContainer();   
            }
            catch (InvalidOperationException)
            {
                error = true;
            }

            Assert.False(error);
        }
    }
}
