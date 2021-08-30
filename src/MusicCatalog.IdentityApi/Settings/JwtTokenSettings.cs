namespace MusicCatalog.IdentityApi.Settings
{
    /// <summary>
    /// Stores authorization options
    /// </summary>
    public class JwtTokenSettings
    {
        /// <summary>
        /// Token publisher
        /// </summary>
        public const string Issuer = "AuthServer";

        /// <summary>
        /// Token client
        /// </summary>
        public const string Audience = "AuthClient";

        /// <summary>
        /// Encryption key
        /// </summary>
        public const string Key = "examplekey";

        /// <summary>
        /// Token lifetime (minutes)
        /// </summary>
        public const int Lifetime = 1;
    }
}
