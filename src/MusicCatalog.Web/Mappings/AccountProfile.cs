using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MusicCatalog.Web.ViewModels;

namespace MusicCatalog.Web.Mappings
{
    /// <summary>
    /// Configures mapping for types
    /// </summary>
    public class AccountProfile : Profile
    {
        /// <summary>
        /// AccountProfile constructor
        /// </summary>
        public AccountProfile()
        {
            CreateMap<IdentityUser, UserViewModel>();
            CreateMap<UserViewModel, IdentityUser>();
        }
    }
}
