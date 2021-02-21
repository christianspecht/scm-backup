using ScmBackup.Scm;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScmBackup.CompositionRoot
{
    /// <summary>
    /// factory to create IScm instances
    /// </summary>
    internal class ScmFactory : IScmFactory
    {
        private readonly Container container;
        
        public Dictionary<ScmType, Type> Scms { get; private set; }

        public ScmFactory(Container container)
        {
            this.container = container;
            this.Scms = new Dictionary<ScmType, Type>();
        }

        public void Register(Type type)
        {
            if (!typeof(IScm).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(string.Format(Resource.TypeIsNoIScm, type.ToString()));
            }

            var attribute = type.GetTypeInfo().GetCustomAttribute<ScmAttribute>();

            this.container.Register(type);
            this.Scms.Add(attribute.Type, type);
        }

        public IScm Create(ScmType type)
        {
            Type outType;

            if (!this.Scms.TryGetValue(type, out outType))
            {
                throw new InvalidOperationException(string.Format(Resource.ScmDoesntExist, type));
            }

            return (IScm)this.container.GetInstance(outType);
        }
    }
}
