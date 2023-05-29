using C_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Services
{
    public interface IBooksLibraryRepository
    {
        Task<IEnumerable<BooksLibrary>> GetAllBooks();
        Task<BooksLibrary> GetBookById(int bookId);
        Task AddBook(BooksLibrary book);
        Task UpdateBook(BooksLibrary book);
        Task DeleteBook(int bookId);
    }
}
