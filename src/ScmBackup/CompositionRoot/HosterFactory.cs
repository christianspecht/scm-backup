using ScmBackup.Hosters;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScmBackup.CompositionRoot
{
    /// <summary>
    /// factory which creates IHoster instances
    /// </summary>
    internal class HosterFactory : Dictionary<string, Type>, IHosterFactory
    {
        private readonly Container container;

        public HosterFactory(Container container)
        {
            this.container = container;
        }

        public void Register<T>() where T : IHoster
        {
            var attribute = typeof(T).GetTypeInfo().GetCustomAttribute<HosterAttribute>();

            this.container.Register(typeof(T));
            this.Add(attribute.Name, typeof(T));
        }

        public IHoster Create(string hosterName)
        {
            Type type;

            if (!this.TryGetValue(hosterName, out type))
            {
                throw new InvalidOperationException(string.Format(Resource.GetString("HosterDoesntExist"), hosterName));
            }

            return (IHoster)this.container.GetInstance(type);
        }
    }
}
