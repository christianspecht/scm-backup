namespace ScmBackup.Scm
{
    internal class ScmCredentials
    {
        public string User { get; private set; }
        public string Password { get; private set; }

        public ScmCredentials(string user, string pass)
        {
            this.User = user;
            this.Password = pass;
        }
    }
}
