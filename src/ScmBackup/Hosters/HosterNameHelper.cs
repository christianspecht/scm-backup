using System;

namespace ScmBackup.Hosters
{
    public static class HosterNameHelper
    {
        /// <summary>
        /// Gets a hoster name from a type name via convention (the part before the suffix is the hoster name)
        /// </summary>
        public static string GetHosterName(Type type, string suffix)
        {
            var name = type.Name.ToLower();
            suffix = suffix.ToLower();

            int n = name.IndexOf(suffix);
            int n2 = name.LastIndexOf(suffix);

            if (n == 0)
            {
                throw new InvalidOperationException(string.Format(Resource.HosterNameError, type.Name, suffix) + Resource.HosterNameError_NoSuffix);
            }

            if (!name.EndsWith(suffix))
            {
                throw new InvalidOperationException(string.Format(Resource.HosterNameError, type.Name, suffix)+ Resource.HosterNameError_End);
            }

            if (n != n2)
            {
                throw new InvalidOperationException(string.Format(Resource.HosterNameError, type.Name, suffix) + Resource.HosterNameError_MultiSuffix);
            }
            
            return name.Substring(0, n);
        }
    }
}
