using System;
using System.Collections.Generic;
using ScmBackup.Hosters;

namespace ScmBackup
{
    /// <summary>
    /// factory which creates BaseHoster instances
    /// </summary>
    internal class HosterFactory : Dictionary<string, BaseHoster>, IHosterFactory
    {
        public void Add(BaseHoster hoster)
        {
            this.Add(hoster.Name, hoster);
        }

        public BaseHoster Create(string hosterName)
        {
            BaseHoster result;

            if (!this.TryGetValue(hosterName, out result))
            {
                throw new InvalidOperationException(string.Format(Resource.GetString("HosterDoesntExist"), hosterName));
            }

            return result;
        }
    }
}
