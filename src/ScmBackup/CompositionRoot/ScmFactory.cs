using ScmBackup.Configuration;
using ScmBackup.Scm;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ScmBackup.CompositionRoot
{
    /// <summary>
    /// factory to create IScm instances
    /// </summary>
    internal class ScmFactory : IScmFactory
    {
        private readonly Container container;
        private Dictionary<Type, ScmType> tempScms; // reverse order because there can be multiple implementations of the same SCM

        public Dictionary<ScmType, Type> Scms { get; private set; }

        public const string DefaultGitImplementation = "GitScm";

        public ScmFactory(Container container)
        {
            this.container = container;
            this.Scms = new Dictionary<ScmType, Type>();
            this.tempScms = new Dictionary<Type, ScmType>();
        }

        public void Register(Type type)
        {
            if (!typeof(IScm).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(string.Format(Resource.TypeIsNoIScm, type.ToString()));
            }

            var attribute = type.GetTypeInfo().GetCustomAttribute<ScmAttribute>();

            this.container.Register(type);
            this.tempScms.Add(type, attribute.Type);
        }

        public IScm Create(ScmType type)
        {
            // fill Scms before first use
            if (!this.Scms.Any())
            {
                var context = (IContext)this.container.GetInstance(typeof(IContext));
                this.Scms = this.FillScms(context.Config, this.tempScms);
            }


            Type outType;

            if (!this.Scms.TryGetValue(type, out outType))
            {
                throw new InvalidOperationException(string.Format(Resource.ScmDoesntExist, type));
            }

            return (IScm)this.container.GetInstance(outType);
        }

        // Return the SCMs that will actually be used (depending on configuration)
        public Dictionary<ScmType, Type> FillScms(Config config, Dictionary<Type, ScmType> tempScms)
        {
            string gitImplementation = config.Options.Git.Implementation;
            if (string.IsNullOrWhiteSpace(gitImplementation))
            {
                gitImplementation = ScmFactory.DefaultGitImplementation;
            }

            var scms = new Dictionary<ScmType, Type>();

            foreach (var tmp in tempScms)
            {
                if ((tmp.Value == ScmType.Git && tmp.Key.Name == gitImplementation) || tmp.Value != ScmType.Git)
                {
                    scms.Add(tmp.Value, tmp.Key);
                }
            }

            return scms;
        }
    }
}
