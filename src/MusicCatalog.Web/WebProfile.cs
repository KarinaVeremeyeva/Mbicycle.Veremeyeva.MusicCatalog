using AutoMapper;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.IdentityApi.Entities;
using MusicCatalog.Web.ViewModels;

namespace MusicCatalog.Web
{
    /// <summary>
    /// Configures mapping for types
    /// </summary>
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<AlbumDto, AlbumViewModel>();
            CreateMap<GenreDto, GenreViewModel>();
            CreateMap<PerformerDto, PerformerViewModel>();
            CreateMap<SongDto, SongViewModel>()
                .ForMember(
                    dest => dest.AlbumId,
                    opt => opt.MapFrom(src => src.AlbumId))
                .ForMember(
                    dest => dest.GenreId,
                    opt => opt.MapFrom(src => src.GenreId))
                .ForMember(
                     dest => dest.PerformerId,
                    opt => opt.MapFrom(src => src.PerformerId));
            CreateMap<RegisterViewModel, User>()
                .ForMember(
                    dest => dest.Login,
                    opt => opt.MapFrom(src => src.Email));

            CreateMap<AlbumViewModel, AlbumDto>();
            CreateMap<GenreViewModel, GenreDto>();
            CreateMap<PerformerViewModel, PerformerDto>();
            CreateMap<SongViewModel, SongDto>();
        }
    }
}
