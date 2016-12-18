using System;
using System.Linq;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class ConfigReaderTests
    {
        [Fact]
        public void ReadsTestConfigFile()
        {
            var sut = new ConfigReader();
            sut.ConfigFileName = "testsettings.json";

            var config = sut.ReadConfig();

            Assert.Equal("localfolder", config.LocalFolder);
            Assert.Equal(5, config.WaitSecondsOnError);
            Assert.Equal(2, config.Sources.Count());

            var source0 = config.Sources[0];
            Assert.Equal("hoster0", source0.Hoster);
            Assert.Equal("type0", source0.Type);
            Assert.Equal("name0", source0.Name);

            var source1 = config.Sources[1];
            Assert.Equal("hoster1", source1.Hoster);
            Assert.Equal("type1", source1.Type);
            Assert.Equal("name1", source1.Name);
        }

        [Fact]
        public void ThrowsExceptionWhenConfigFileIsNotVaildJson()
        {
            var sut = new ConfigReader();
            sut.ConfigFileName = "brokensettings.json";

            Assert.ThrowsAny<FormatException>(() => sut.ReadConfig());
        }
    }
}
