using System;

namespace ScmBackup.Hosters
{
    public class HosterNameHelper
    {
        /// <summary>
        /// Gets a hoster name from a type name via convention (the part before the suffix is the hoster name)
        /// </summary>
        public string GetHosterName(Type type, string suffix)
        {
            var name = type.Name.ToLower();
            suffix = suffix.ToLower();

            if (!name.EndsWith(suffix))
            {
                throw new InvalidOperationException(string.Format(Resource.CouldntGetHosterName, type.Name));
            }

            int n = name.IndexOf(suffix);
            return name.Substring(0, n);
        }
    }
}
