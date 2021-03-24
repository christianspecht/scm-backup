using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ScmBackup.Tests
{
    public class ErrorLevelTests
    {
        [Fact]
        public void LevelName_ReturnsName()
        {
            var sut = ErrorLevel.Info;

            Assert.Equal("Info", sut.LevelName());
        }
    }
}
