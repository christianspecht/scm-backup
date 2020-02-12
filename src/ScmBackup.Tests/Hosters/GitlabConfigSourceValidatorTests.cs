using ScmBackup.Hosters.Gitlab;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Tests.Hosters
{
    public class GitlabConfigSourceValidatorTests : IConfigSourceValidatorTests
    {
        public GitlabConfigSourceValidatorTests()
        {
            config = new ConfigSource();
            config.Hoster = "gitlab";
            config.Type = "user";
            config.Name = "foo";
            config.AuthName = config.Name;
            config.Password = "pass";

            sut = new GitlabConfigSourceValidator();
        }
    }
}
