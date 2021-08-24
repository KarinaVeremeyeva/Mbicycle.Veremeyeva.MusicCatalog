using AutoMapper;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.DataAccess.Entities;

namespace MusicCatalog.BusinessLogic
{
    /// <summary>
    /// Configures mapping for types
    /// </summary>
    public class BLLProfile : Profile
    {
        public BLLProfile()
        {
            this.CreateMap<Album, AlbumDto>();
            this.CreateMap<Genre, GenreDto>();
            this.CreateMap<Performer, PerformerDto>();
            this.CreateMap<Song, SongDto>();

            this.CreateMap<AlbumDto, Album>();
            this.CreateMap<GenreDto, Genre> ();
            this.CreateMap<PerformerDto, Performer>();
            this.CreateMap<SongDto, Song>();
        }
    }
}
