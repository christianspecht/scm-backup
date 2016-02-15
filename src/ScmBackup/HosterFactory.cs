using System.Collections.Generic;

namespace ScmBackup
{
    /// <summary>
    /// factory which creates IHoster instances
    /// </summary>
    public class HosterFactory : Dictionary<string, IHoster>, IHosterFactory
    {
        public void Add(IHoster hoster)
        {
            this.Add(hoster.Name, hoster);
        }

        public IHoster Create(string hosterName)
        {
            return this[hosterName];
        }
    }
}
