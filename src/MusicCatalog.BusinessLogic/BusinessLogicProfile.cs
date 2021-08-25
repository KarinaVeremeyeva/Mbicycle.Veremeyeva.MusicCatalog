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
        public BusinessLogicProfile()
        {
            CreateMap<Album, AlbumDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Performer, PerformerDto>();
            CreateMap<Song, SongDto>();

            CreateMap<AlbumDto, Album>();
            CreateMap<GenreDto, Genre> ();
            CreateMap<PerformerDto, Performer>();
            CreateMap<SongDto, Song>();
        }
    }
}
