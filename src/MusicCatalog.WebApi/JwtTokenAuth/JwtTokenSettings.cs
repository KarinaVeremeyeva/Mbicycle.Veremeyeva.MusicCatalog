namespace MusicCatalog.WebApi.JwtTokenAuth
{
    /// <summary>
    /// Stores jwt token settings
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
        public string JwtSecretKey { get; set; }
    }
}
