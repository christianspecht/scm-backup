using MailKit.Net.Smtp;
using MimeKit;

namespace ScmBackup.Http
{
    internal class MailKitEmailSender : IEmailSender
    {
        private readonly IContext context;

        public MailKitEmailSender(IContext context)
        {
            this.context = context;
        }

        public void Send(string subject, string body)
        {
            var config = this.context.Config.Email;

            if (config == null)
            {
                return;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.From));
            message.To.Add(new MailboxAddress(config.To));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                // TODO: copied from MailKit's docs, not sure if we need this
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(config.Server, config.Port, config.UseSsl);
                
                client.Authenticate(config.UserName, config.Password);

                client.Send(message);
                client.Disconnect(true);
            }

        }
    }
}
