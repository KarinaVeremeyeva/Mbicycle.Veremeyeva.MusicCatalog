using System.Collections.Generic;

namespace MusicCatalog.IdentityApi.Entities
{
    /// <summary>
    /// Represents user's role
    /// </summary>
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
