using System;
using System.Collections.Generic;
using ScmBackup.Hosters;

namespace ScmBackup
{
    /// <summary>
    /// factory which creates BaseHoster instances
    /// </summary>
    internal class HosterFactory : Dictionary<string, IHoster>, IHosterFactory
    {
        public void Add(IHoster hoster)
        {
            this.Add(hoster.Name, hoster);
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
