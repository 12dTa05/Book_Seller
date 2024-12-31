using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewsModel;
using BookSale.Management.DataAccess.Repository;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookSale.Management.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseDatatable<GenreDTO>> GetGenreByPagination(RequestDatatable requestDatatable)
        {
            var genres = await _unitOfWork.GerneRepository.GetAllGenre();

            var gernesDto = _mapper.Map<IEnumerable<GenreDTO>>(genres);

            int totalRecords = gernesDto.Count();

            var results = gernesDto.Skip(requestDatatable.SkipItems).Take(requestDatatable.PageSize).ToList();



            return new ResponseDatatable<GenreDTO>
            {
                Draw = requestDatatable.Draw,
                Data = results,
                RecordsFiltered = totalRecords,
                RecordsTotal = totalRecords
            };


        }
        public async Task<GenreViewModel> GetById(int id)
        {
            var genre = await _unitOfWork.GerneRepository.GetById(id);

            return _mapper.Map<GenreViewModel>(genre);
        }
        public async Task<IEnumerable<SelectListItem>> GetGenresForDropdownlistAsync()
        {
            var genres = await _unitOfWork.GerneRepository.GetAllGenre();
            
            if (genres == null || !genres.Any())
                return new List<SelectListItem>();
            
            return genres.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name ?? "Unknown"
            }).ToList();
        }
        public IEnumerable<GenreSiteDTO>  GetGenresListForSiteAsync()
        {
            var result = _unitOfWork.GerneRepository.Table.Select(x => new GenreSiteDTO{
                Id = x.Id,
                Name = x.Name,
                TotalBooks = x.Books.Count
            });

            return result;
        }
    }
}
