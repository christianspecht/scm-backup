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

        public void Register(Type type)
        {
            if (!typeof(IHoster).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(string.Format(Resource.TypeIsNoIHoster, type.ToString()));
            }

            string hosterName = HosterNameHelper.GetHosterName(type, "hoster");

            this.container.Register(type);
            this.Add(hosterName, type);
        }

        public IHoster Create(string hosterName)
        {
            Type type;

            if (!this.TryGetValue(hosterName, out type))
            {
                throw new InvalidOperationException(string.Format(Resource.HosterDoesntExist, hosterName));
            }

            return (IHoster)this.container.GetInstance(type);
        }
    }
}
