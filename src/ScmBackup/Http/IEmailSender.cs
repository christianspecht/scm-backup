namespace ScmBackup.Http
{
    internal interface IEmailSender
    {
        void Send( string subject, string body);
    }
}
