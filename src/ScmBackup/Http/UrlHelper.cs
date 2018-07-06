using System;

namespace ScmBackup.Http
{
    internal class UrlHelper
    {
        public bool UrlIsValid(string url)
        {
            // https://stackoverflow.com/a/7581824/6884
            // (Uri.UriSchemeHttp and Uri.UriSchemeHttps replaced by strings because apparently they don't exist in .NET Core)
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == "http" || uriResult.Scheme == "https");
        }
    }
}
