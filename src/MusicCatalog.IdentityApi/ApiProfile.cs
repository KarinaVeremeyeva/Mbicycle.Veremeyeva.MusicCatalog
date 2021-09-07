using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MusicCatalog.IdentityApi.Models;

namespace MusicCatalog.IdentityApi
{
    /// <summary>
    /// Configures mapping for types
    /// </summary>
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<UserModel, IdentityUser>();
            CreateMap<RegisterModel, IdentityUser>();
            CreateMap<LoginModel, IdentityUser>();

            CreateMap<IdentityUser, RegisterModel>();
            CreateMap<IdentityUser, UserModel>();
            CreateMap<IdentityUser, LoginModel>();
        }
    }
}
