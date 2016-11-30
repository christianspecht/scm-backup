using System.Collections.Generic;
using System.Linq;

namespace ScmBackup
{
    /// <summary>
    /// return value for validators
    /// </summary>
    internal class ValidationResult
    {
        private readonly ConfigSource source;

        public ValidationResult() : this(null)
        {
        }

        public ValidationResult(ConfigSource configSource)
        {
            this.Messages = new List<ValidationMessage>();
            this.source = configSource;
        }

        public bool IsValid
        {
            get
            {
                return !this.Messages.Any(m => m.Error == ErrorLevel.Error);
            }
        }

        public List<ValidationMessage> Messages { get; private set; }

        public void AddMessage(ErrorLevel error, string message)
        {
            if (this.source != null)
            {
                message = this.source.Title + ": " + message;
            }

            this.Messages.Add(new ValidationMessage(error, message));
        }

        internal class ValidationMessage
        {
            public ValidationMessage(ErrorLevel error, string message)
            {
                this.Error = error;
                this.Message = message;
            }

            public ErrorLevel Error { get; private set; }
            public string Message { get; private set; }
        }
    }
}
