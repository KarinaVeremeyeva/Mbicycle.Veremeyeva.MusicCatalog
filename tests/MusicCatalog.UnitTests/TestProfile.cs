﻿using AutoMapper;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.ViewModels;

namespace MusicCatalog.UnitTests
{
    public class TestProfile : Profile
    {
        public TestProfile()
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

            CreateMap<AlbumViewModel, AlbumDto>();
            CreateMap<GenreViewModel, GenreDto>();
            CreateMap<PerformerViewModel, PerformerDto>();
        }
    }
}
