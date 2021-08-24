using AutoMapper;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.ViewModels;

namespace MusicCatalog.UnitTests
{
    public class TestProfile : Profile
    {
        public TestProfile()
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

            this.CreateMap<AlbumViewModel, AlbumDto>();
            this.CreateMap<GenreViewModel, GenreDto>();
            this.CreateMap<PerformerViewModel, PerformerDto>();
            this.CreateMap<SongViewModel, SongDto>();
        }
    }
}
