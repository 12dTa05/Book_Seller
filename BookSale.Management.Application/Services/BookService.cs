using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.ViewsModel;
using BookSale.Management.DataAccess.Repository;
using BookSale.Management.Domain.Entities;
using BookSale.Management.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICommonService _commonService;
        public BookService(IUnitOfWork unitOfWork, IMapper mapper, ICommonService commonService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _commonService = commonService;
        }

        public async Task<ResponseDatatable<BookDTO>> GetBooksByPaginationAsync(RequestDatatable requestDatatable)
        {
            
            int totalRecords = 0;
            IEnumerable<BookDTO> books;

            (books, totalRecords) = await _unitOfWork.BookRepository.GetBooksByPagination<BookDTO>(requestDatatable.SkipItems, requestDatatable.PageSize, requestDatatable.Keyword);

            return new ResponseDatatable<BookDTO>
            {
                Draw = requestDatatable.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = totalRecords,
                Data = books
            };

        }

        public async Task<BookViewModel> GetBooksByIdAsync(int id)
        {
            var book = await _unitOfWork.BookRepository.GetBooksByIdAsync(id);
            if (book == null)
                return new BookViewModel();

            return _mapper.Map<BookViewModel>(book);
        }

        public async Task<ResponseModel> SaveAsync(BookViewModel bookVM)
        {
            var book = _mapper.Map<Book>(bookVM);
            if (bookVM.Id == 0)
            {


                book.CreatedOn = DateTime.Now;
                book.IsActive = true;
                book.Code = bookVM.Code;
            }
            var result = await _unitOfWork.BookRepository.Save(book);

            await _unitOfWork.Commit();

            var actionType = bookVM.Id == 0 ? ActionType.Insert : ActionType.Update;
            var successMessage = $"{(bookVM.Id == 0 ? "Insert" : "Update")} successfully.";
            var failureMessage = $"{(bookVM.Id == 0 ? "Insert" : "Update")} failed.";

            return new ResponseModel
            {
                Action = actionType,
                Message = result ? successMessage : failureMessage,
                Status = result,
            };
        }

        public async Task<string> GenerateCodeAsync(int number = 10)
        {
            string newCode = string.Empty;
            while (true)
            {
                newCode = _commonService.GenerateRandomCode(number);

                var bookCode = await _unitOfWork.BookRepository.GetBooksByCodeAsync(newCode);
                if (bookCode is null)
                {
                    break;
                }
            }

            return newCode;
        }

        public async Task<(IEnumerable<BookDTO>, int)> GetBooksForSiteAsync(int genreId, int pageIndex, int pageSize = 10)
        {
            
            int totalRecords = 0;
            IEnumerable<Book> books;

            (books, totalRecords) = await _unitOfWork.BookRepository.GetBooksForSite<BookDTO>(genreId, pageIndex, pageSize);

            var booksDTO = _mapper.Map<IEnumerable<BookDTO>>(books);

            return (booksDTO, totalRecords);

        }
        public async Task<IEnumerable<BookCartDTO>> GetBooksByListCodeAsync(string[] codes)
        {
            var books = await _unitOfWork.BookRepository.GetBooksByListCodeAsync(codes);
            var result = _mapper.Map<IEnumerable<BookCartDTO>>(books);
            
            return result;
        }
    }
}
