namespace ScmBackup
{
    /// <summary>
    /// Used to distinguish between different validation messages.
    /// Not really elegant, but the only way to test whether the validation returned a specific message.
    /// </summary>
    public enum ValidationMessageType
    {
        WrongHoster,
        WrongType,
        NameEmpty,
        AuthNameOrPasswortEmpty,
        AuthNameAndPasswortEmpty,
        AuthNameAndNameNotEqual,

        /// <summary>
        /// default value when not set - sometimes we don't care
        /// </summary>
        Undefined
    }
}
