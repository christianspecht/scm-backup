using ScmBackup.Configuration;
using Xunit;
using YamlDotNet.Core;

namespace ScmBackup.Tests.Integration
{
    public class ConfigReaderTests
    {
        [Fact]
        public void ReadsTestConfigFile()
        {
            var sut = new ConfigReader();
            sut.ConfigFileName = "testsettings.yml";

            var config = sut.ReadConfig();

            Assert.Equal("localfolder", config.LocalFolder);
            Assert.Equal(5, config.WaitSecondsOnError);
            Assert.Equal(2, config.Sources.Count);
            Assert.Equal(2, config.Scms.Count);

            var source0 = config.Sources[0];
            Assert.Equal("hoster0", source0.Hoster);
            Assert.Equal("type0", source0.Type);
            Assert.Equal("name0", source0.Name);

            var source1 = config.Sources[1];
            Assert.Equal("hoster1", source1.Hoster);
            Assert.Equal("type1", source1.Type);
            Assert.Equal("name1", source1.Name);

            var ignores = source1.IgnoreRepos;
            Assert.Equal(2, ignores.Count);
            Assert.Equal("ignore0", ignores[0]);
            Assert.Equal("ignore1", ignores[1]);

            var scm1 = config.Scms[0];
            Assert.Equal("git", scm1.Name);
            Assert.Equal("path/to/git", scm1.Path);

            var scm2 = config.Scms[1];
            Assert.Equal("hg", scm2.Name);
            Assert.Equal("path/to/hg", scm2.Path);
        }

        [Fact]
        public void ThrowsExceptionWhenConfigFileIsNotVaild()
        {
            var sut = new ConfigReader();
            sut.ConfigFileName = "brokensettings.yml";

            Assert.ThrowsAny<YamlException>(() => sut.ReadConfig());
        }
    }
}
