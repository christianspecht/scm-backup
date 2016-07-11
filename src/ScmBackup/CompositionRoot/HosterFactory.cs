using ScmBackup.Hosters;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScmBackup.CompositionRoot
{
    /// <summary>
    /// factory which creates IHoster instances
    /// </summary>
    internal class HosterFactory : Dictionary<string, IHoster>, IHosterFactory
    {
        public void Add(IHoster hoster)
        {
            var attribute = hoster.GetType().GetTypeInfo().GetCustomAttribute<HosterAttribute>();

            this.Add(attribute.Name, hoster);
        }

        public IHoster Create(string hosterName)
        {
            IHoster result;

            if (!this.TryGetValue(hosterName, out result))
            {
                throw new InvalidOperationException(string.Format(Resource.GetString("HosterDoesntExist"), hosterName));
            }

            return result;
        }
    }
}
