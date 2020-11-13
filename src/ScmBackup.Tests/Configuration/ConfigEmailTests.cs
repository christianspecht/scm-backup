using ScmBackup.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ScmBackup.Tests.Configuration
{
    public class ConfigEmailTests
    {
        [Fact]
        public void ListContainsSingleEmail()
        {
            var sut = new ConfigEmail();
            sut.To = "foo@example.com";

            var result = sut.To_AsList();

            Assert.Single(result);
            Assert.Equal("foo@example.com", result.First());
        }

        [Fact]
        public void ListContainsMultipleEmails()
        {
            var sut = new ConfigEmail();
            sut.To = "1@example.com;2@example.com;3@example.com";

            var result = sut.To_AsList();
            Assert.Equal(3, result.Count());
            Assert.Contains("1@example.com", result);
            Assert.Contains("2@example.com", result);
            Assert.Contains("3@example.com", result);
        }
    }
}
