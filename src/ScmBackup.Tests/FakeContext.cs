﻿using ScmBackup.Configuration;
using System;

namespace ScmBackup.Tests
{
    internal class FakeContext : IContext
    {
        public FakeContext()
        {
            this.VersionNumber = new Version(0, 0, 0);
            this.VersionNumberString = this.VersionNumber.ToString();
            this.AppTitle = "SCM Backup";
            this.UserAgent = "SCM-Backup";

            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            this.Config = reader.ReadConfig();
        }

        public Version VersionNumber { get; set; }

        public string VersionNumberString { get; set; }

        public string AppTitle { get; set; }

        public string UserAgent { get; set; }

        public Config Config { get; set; }

        public static FakeContext BuildFakeContextWithConfig(Config config)
        {
            var context = new FakeContext();
            context.Config = config;
            return context;
        }
    }
}
