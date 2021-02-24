﻿using ScmBackup.CompositionRoot;
using ScmBackup.Scm;
using SimpleInjector;
using System;
using System.Collections.Generic;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class ScmFactoryTests
    {
        private readonly ScmFactory sut;

        public ScmFactoryTests()
        {
            var container = new Container();
            container.Register<IFileSystemHelper, FileSystemHelper>();
            container.Register<IContext, FakeContext>();

            sut = new ScmFactory(container);
            sut.Register(typeof(GitScm));
        }

        [Fact]
        public void CreateReturnScm()
        {
            var result = sut.Create(ScmType.Git);

            Assert.NotNull(result);
            Assert.True(result is IScm);
        }

        [Fact]
        public void RegisterThrowsIfRegisteredTypeIsNotIScm()
        {
            Assert.Throws<InvalidOperationException>(() => sut.Register(typeof(ScmBackup)));
        }

        [Fact]
        public void FillScmsReturnsStandardGit()
        {
            var tempScms = new Dictionary<Type, ScmType>();
            tempScms.Add(typeof(GitScm), ScmType.Git);
            tempScms.Add(typeof(LibgitScm), ScmType.Git);

            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();
            var config = reader.ReadConfig();

            var result = sut.FillScms(config, tempScms);

            var gitImplementation = result[ScmType.Git].Name;
            Assert.Equal(ScmFactory.DefaultGitImplementation, gitImplementation);
        }

        [Fact]
        public void FillScmsReturnsGitFromConfig()
        {
            var tempScms = new Dictionary<Type, ScmType>();
            tempScms.Add(typeof(GitScm), ScmType.Git);
            tempScms.Add(typeof(LibgitScm), ScmType.Git);

            var reader = new FakeConfigReader();
            reader.SetDefaultFakeConfig();

            string expectedGitImplementation = "LibgitScm";

            var gitConfig = new Dictionary<string, object>();
            gitConfig.Add("implementation", expectedGitImplementation);
            reader.FakeConfig.Options.Add("git", gitConfig);
            var config = reader.ReadConfig();

            var result = sut.FillScms(config, tempScms);

            var gitImplementation = result[ScmType.Git].Name;
            Assert.Equal(expectedGitImplementation, gitImplementation);
        }

    }
}
