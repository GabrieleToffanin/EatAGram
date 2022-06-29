namespace Eatagram.Core.Configuration
{
    /// <summary>
    /// Default user credentials for having an already registered user
    /// </summary>
    public static class ApplicationIdenityConstants
    {
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string Member = "Member";
        }

        public const string DefaultPassword = "Unicorn-12";

    }
}
