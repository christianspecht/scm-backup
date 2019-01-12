using ScmBackup.Configuration;
using System;

namespace ScmBackup.CompositionRoot
{
    internal class HosterValidator : IHosterValidator
    {
        private readonly IHosterFactory factory;

        public HosterValidator(IHosterFactory factory)
        {
            this.factory = factory;
        }

        public ValidationResult Validate(ConfigSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource.ConfigSourceIsNull);
            }

            var hoster = this.factory.Create(source.Hoster);
            return hoster.Validator.Validate(source);
        }
    }
}
