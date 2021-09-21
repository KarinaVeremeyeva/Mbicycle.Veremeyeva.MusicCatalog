using AutoMapper;
using MusicCatalog.IdentityApi.Models;

namespace MusicCatalog.WebApi.Mappings
{
    /// <summary>
    /// Configures mapping for types
    /// </summary>
    public class UserProfile : Profile
    {
        /// <summary>
        /// UserProfile constructor
        /// </summary>
        public UserProfile()
        {
            CreateMap<RegisterViewModel, RegisterModel>();
            CreateMap<LoginViewModel, LoginModel>();
            CreateMap<UserViewModel, UserModel>();
            CreateMap<UserModel, UserViewModel>();
        }
    }
}
