using Xunit;

namespace ScmBackup.Tests
{
    public class ConfigSourceTests
    {
        [Fact]
        public void ConfigSourcesWithSameNameAreEqual()
        {
            var source1 = new ConfigSource();
            source1.Name = "foo";

            var source2 = new ConfigSource();
            source2.Name = "foo";

            Assert.True(source1.Equals(source2), "Equals");
            Assert.True(object.Equals(source1, source2), "object.Equals");
            Assert.True(source1.GetHashCode() == source2.GetHashCode(), "GetHashCode");
        }
    }
}
