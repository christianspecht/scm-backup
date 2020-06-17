using ScmBackup.Http;
using System;
using System.IO;

namespace ScmBackup.Scm
{
    [Scm(Type = ScmType.Mercurial)]
    internal class MercurialScm : CommandLineScm, IScm
    {
        public MercurialScm(IFileSystemHelper filesystemhelper, IContext context, IUrlHelper urlhelper)
        {
            this.FileSystemHelper = filesystemhelper;
            this.context = context;
            this.UrlHelper = urlhelper;
        }

        public IFileSystemHelper FileSystemHelper { get; set; }
        public IUrlHelper UrlHelper { get; set; }

        public override string ShortName
        {
            get { return "hg"; }
        }

        public override string DisplayName
        {
            get { return "Mercurial"; }
        }

        protected override string CommandName
        {
            get { return "hg"; }
        }

        public override bool IsOnThisComputer()
        {
            var result = this.ExecuteCommand("version");

            if (result.Successful && result.StandardOutput.ToLower().Contains("mercurial"))
            {
                return true;
            }

            return false;
        }

        public override string GetVersionNumber()
        {
            var result = this.ExecuteCommand("version -T {ver}");

            if (result.Successful)
            {
                return result.StandardOutput;
            }

            throw new InvalidOperationException(result.Output);
        }

        public override bool DirectoryIsRepository(string directory)
        {
            string hgdir = Path.Combine(directory, ".hg");
            return Directory.Exists(hgdir);
        }

        public override void CreateRepository(string directory)
        {
            if (!this.DirectoryIsRepository(directory))
            {
                string cmd = string.Format("init \"{0}\"", directory);
                var result = this.ExecuteCommand(cmd);
                if (!result.Successful)
                {
                    throw new InvalidOperationException(result.Output);
                }
            }
        }

        public override bool RemoteRepositoryExists(string remoteUrl, ScmCredentials credentials)
        {
            remoteUrl = this.RemoveCredentialsFromUrl(remoteUrl, credentials);

            string cmd = "identify " + remoteUrl;
            if (credentials != null)
            {
                cmd += this.CreateParametersWithCredentials(credentials, remoteUrl);
            }

            var result = this.ExecuteCommand(cmd);

            return result.Successful;
        }

        public override void PullFromRemote(string remoteUrl, string directory, ScmCredentials credentials)
        {
            if (!this.DirectoryIsRepository(directory))
            {
                if (Directory.Exists(directory) && !this.FileSystemHelper.DirectoryIsEmpty(directory))
                {
                    throw new InvalidOperationException(string.Format(Resource.ScmTargetDirectoryNotEmpty, directory));
                }

                this.CreateRepository(directory);
            }

            remoteUrl = this.RemoveCredentialsFromUrl(remoteUrl, credentials);

            string cmd = string.Format("pull {0} -R \"{1}\"", remoteUrl, directory);
            if (credentials != null)
            {
                cmd += this.CreateParametersWithCredentials(credentials, remoteUrl);
            }

            var result = this.ExecuteCommand(cmd);

            if (!result.Successful)
            {
                throw new InvalidOperationException(result.Output);
            }
        }

        public override bool RepositoryContainsCommit(string directory, string commitid)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException(string.Format(Resource.DirectoryDoesntExist, directory));
            }

            if (!this.DirectoryIsRepository(directory))
            {
                throw new InvalidOperationException(string.Format(Resource.DirectoryNoRepo, directory));
            }

            string cmd = string.Format("log -R \"{0}\" -r {1} -T {{node}}", directory, commitid);
            var result = this.ExecuteCommand(cmd);

            if (result.Successful && result.Output.StartsWith(commitid))
            {
                return true;
            }

            return false;
        }

        private string RemoveCredentialsFromUrl(string url, ScmCredentials credentials)
        {
            // Issue #19: if credentials are passed via --config, remove the username from the URL (the Bitbucket API returns the clone URL with username, for example).
            // Some HG versions stop when the username is in the URL *and* passed via --config
            if (credentials != null)
            {
                return this.UrlHelper.RemoveCredentialsFromUrl(url);
            }

            return url;
        }

        private string CreateParametersWithCredentials(ScmCredentials credentials, string url)
        {
            // pass credentials via command line instead of putting them into the URL: https://stackoverflow.com/a/22126365/6884

            // Note: we need to pass the host from the URL (e.g. https://bitbucket.org) as the first parameter, otherwise 
            // authentication won't work when there's an actual entry in the local config file for the same hoster.
            var uri = new Uri(url);
            string baseurl = new UriBuilder(uri.Scheme, uri.Host).ToString();
            
            return string.Format(" --config auth.x.prefix={0} --config auth.x.username={1} --config auth.x.password={2}", baseurl, credentials.User, credentials.Password);
        }
    }
}
