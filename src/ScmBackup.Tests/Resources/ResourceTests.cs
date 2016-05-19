using System;
using System.Globalization;
using Xunit;

namespace ScmBackup.Tests.Resources
{
    public class ResourceTests
    {
        [Fact]
        public void ThrowsWhenInitializedWithoutProvider()
        {
            Assert.Throws<ArgumentNullException>(() => Resource.Initialize(null, new CultureInfo("en-US")));
        }

        [Fact]
        public void InitializeCallsUnderlyingProvider()
        {
            var provider = new FakeResourceProvider();
            var culture = new CultureInfo("en-US");

            Resource.Initialize(provider, culture);

            Assert.True(provider.InitializeWasCalled);
            Assert.Equal(culture, provider.LastCulture);

            Resource.ResetToDefaultProvider();
        }

        [Fact]
        public void GetStringReturnsStringWhenInitialized()
        {
            var provider = new FakeResourceProvider();
            provider.StringToReturn = "foo";
            var culture = new CultureInfo("en-US");
            Resource.Initialize(provider, culture);

            string result = Resource.GetString("bar");

            Assert.True(provider.GetStringWasCalled);
            Assert.Equal("bar", provider.LastKey);
            Assert.Equal(provider.StringToReturn, result);

            Resource.ResetToDefaultProvider();
        }

        [Fact]
        public void GetStringReturnsEmptyStringInsteadOfNull()
        {
            // ...when the key is not found or the resource string is actually Null.
            // This would cause problems all over the place when formatting strings.
            var provider = new FakeResourceProvider();
            provider.StringToReturn = null;
            var culture = new CultureInfo("en-US");
            Resource.Initialize(provider, culture);

            string result = Resource.GetString("bar");

            Assert.NotNull(result);
            Assert.Equal(string.Empty, result);

            Resource.ResetToDefaultProvider();
        }
    }
}
