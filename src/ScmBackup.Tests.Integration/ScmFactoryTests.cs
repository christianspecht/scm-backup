using ScmBackup.CompositionRoot;
using ScmBackup.Scm;
using SimpleInjector;
using System;
using Xunit;

namespace ScmBackup.Tests.Integration
{
    public class ScmFactoryTests
    {
        private readonly ScmFactory sut;

        public ScmFactoryTests()
        {
            sut = new ScmFactory(new Container());
            sut.Register(typeof(GitScm));
        }

        [Fact]
        public void NewScmIsAdded()
        {
            Assert.Equal(1, sut.Count);
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
    }
}
