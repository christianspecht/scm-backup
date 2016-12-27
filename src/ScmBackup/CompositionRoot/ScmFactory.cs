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
    internal class ScmFactory : Dictionary<ScmType, Type>,  IScmFactory
    {
        private readonly Container container;

        public ScmFactory(Container container)
        {
            this.container = container;
        }

        public void Register(Type type)
        {
            if (!typeof(IScm).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(string.Format(Resource.TypeIsNoIScm, type.ToString()));
            }

            var attribute = type.GetTypeInfo().GetCustomAttribute<ScmAttribute>();

            this.container.Register(type);
            this.Add(attribute.Type, type);
        }

        public IScm Create(ScmType type)
        {
            Type outType;

            if (!this.TryGetValue(type, out outType))
            {
                throw new InvalidOperationException(string.Format(Resource.ScmDoesntExist, type));
            }

            return (IScm)this.container.GetInstance(outType);
        }
    }
}
