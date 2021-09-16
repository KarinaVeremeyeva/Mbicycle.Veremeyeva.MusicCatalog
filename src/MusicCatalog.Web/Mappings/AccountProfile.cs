using AutoMapper;
using MusicCatalog.IdentityApi.Models;
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
            CreateMap<UserModel, UserViewModel>();
            CreateMap<UserViewModel, UserModel>();

            CreateMap<LoginViewModel, LoginModel>();
            CreateMap<RegisterViewModel, RegisterModel>();
        }
    }
}
