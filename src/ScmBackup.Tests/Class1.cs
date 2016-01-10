using Xunit;

namespace ScmBackup.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Class1
    {
        [Fact]
        public void Test()
        {
            Assert.Equal(2, 1 + 1);
        }
    }
}
