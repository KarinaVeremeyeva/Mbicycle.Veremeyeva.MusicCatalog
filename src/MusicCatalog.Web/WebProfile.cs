using AutoMapper;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.Models;
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
            this.CreateMap<AlbumDto, AlbumViewModel>();
            this.CreateMap<GenreDto, GenreViewModel>();
            this.CreateMap<PerformerDto, PerformerViewModel>();
            this.CreateMap<SongDto, SongViewModel>()
                .ForMember(
                    dest => dest.AlbumId,
                    opt => opt.MapFrom(src => src.AlbumId))
                .ForMember(
                    dest => dest.GenreId,
                    opt => opt.MapFrom(src => src.GenreId))
                .ForMember(
                     dest => dest.GenreId,
                    opt => opt.MapFrom(src => src.PerformerId));
            this.CreateMap<RegisterViewModel, User>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(
                  dest => dest.YearOfBirth,
                    opt => opt.MapFrom(src => src.YearOfBirth));

            this.CreateMap<AlbumViewModel, AlbumDto>();
            this.CreateMap<GenreViewModel, GenreDto>();
            this.CreateMap<PerformerViewModel, PerformerDto>();
            this.CreateMap<SongViewModel, SongDto>();
        }
    }
}
