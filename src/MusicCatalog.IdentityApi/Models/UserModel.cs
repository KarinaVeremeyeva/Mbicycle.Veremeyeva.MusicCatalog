using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalog.IdentityApi.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// User id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }
    }
}
