namespace MusicCatalog.IdentityApi.Settings
{
    /// <summary>
    /// Stores authorization options
    /// </summary>
    public class JwtTokenSettings
    {
        /// <summary>
        /// Token publisher
        public string JwtIssuer { get; set; }

        /// <summary>
        /// Token client
        /// </summary>
        public string JwtAudience { get; set; }

        /// <summary>
        /// Encryption key
        /// </summary>
        public char[] JwtSecretKey { get; set; }
    }
}
