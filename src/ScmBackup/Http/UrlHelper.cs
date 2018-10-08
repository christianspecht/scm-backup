using System;

namespace ScmBackup.Http
{
    internal class UrlHelper : IUrlHelper
    {
        public bool UrlIsValid(string url)
        {
            // https://stackoverflow.com/a/7581824/6884
            // (Uri.UriSchemeHttp and Uri.UriSchemeHttps replaced by strings because apparently they don't exist in .NET Core)
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == "http" || uriResult.Scheme == "https");
        }

        public string RemoveCredentialsFromUrl(string oldUrl)
        {
            var uri = new UriBuilder(oldUrl);
            uri.UserName = null;
            uri.Password = null;
            if (uri.Uri.IsDefaultPort)
            {
                uri.Port = -1;
            }
            return uri.Uri.AbsoluteUri;
        }
    }
}
