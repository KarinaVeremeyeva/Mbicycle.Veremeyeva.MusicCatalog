namespace MusicCatalog.IdentityApi.Entities
{
    /// <summary>
    /// User details for authorization
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Role id
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// User's role
        /// </summary>
        public string Role { get; set; }
    }
}
