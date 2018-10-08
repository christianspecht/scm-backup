namespace ScmBackup.Http
{
    internal interface IUrlHelper
    {
        string RemoveCredentialsFromUrl(string oldUrl);
        bool UrlIsValid(string url);
    }
}