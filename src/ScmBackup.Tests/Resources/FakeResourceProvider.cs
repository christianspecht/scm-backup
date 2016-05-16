using ScmBackup.Resources;
using System.Globalization;

namespace ScmBackup.Tests.Resources
{
    public class FakeResourceProvider : IResourceProvider
    {
        public string LastKey { get; private set; }
        public string StringToReturn { get; set; }
        public bool GetStringWasCalled { get; private set; }

        public CultureInfo LastCulture { get; private set; }
        public bool InitializeWasCalled { get; private set; }

        public string GetString(string key)
        {
            this.GetStringWasCalled = true;
            this.LastKey = key;
            return this.StringToReturn;
        }

        public void Initialize(CultureInfo culture)
        {
            this.InitializeWasCalled = true;
            this.LastCulture = culture;
        }
    }
}
