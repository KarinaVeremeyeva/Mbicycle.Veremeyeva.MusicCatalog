using AutoMapper;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.DataAccess.Entities;

namespace MusicCatalog.BusinessLogic
{
    /// <summary>
    /// Configures mapping for types
    /// </summary>
    public class BusinessLogicProfile : Profile
    {
        /// <summary>
        /// BusinessLogicProfile constructor
        /// </summary>
        public BusinessLogicProfile()
        {
            CreateMap<Album, AlbumDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Performer, PerformerDto>();
            CreateMap<Song, SongDto>()
                .ForMember(
                    dest => dest.AlbumName,
                    opt => opt.MapFrom(src => src.Album.Name))
                .ForMember(
                    dest => dest.GenreName,
                    opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(
                    dest => dest.PerformerName,
                    opt => opt.MapFrom(src => src.Performer.Name));

            CreateMap<AlbumDto, Album>();
            CreateMap<GenreDto, Genre> ();
            CreateMap<PerformerDto, Performer>();
            CreateMap<SongDto, Song>();
        }
    }
}
