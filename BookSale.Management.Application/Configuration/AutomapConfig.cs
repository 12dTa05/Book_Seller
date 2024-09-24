using AutoMapper;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewsModel;
using BookSale.Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Configuration
{
    public class AutomapConfig : Profile
    {
        public AutomapConfig()
        {
            CreateMap<ApplicationUser, AccountDTO>()
                .ForMember(dest => dest.Phone, source => source.MapFrom(x => x.PhoneNumber))
                .ReverseMap();

            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Genre, GenreViewModel>().ReverseMap();
            
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ReverseMap();
            CreateMap<Book, BookViewModel>().ReverseMap();
            CreateMap<Book, BookCartDTO>()
                .ForMember(dest => dest.Price, s => s.MapFrom(x => x.Cost))
                .ReverseMap();
        }
    }
}
