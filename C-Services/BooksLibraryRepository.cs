using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_Data;
using C_Models;
using C_Services;
using Microsoft.EntityFrameworkCore;

public class BooksLibraryRepository : IBooksLibraryRepository
{
    private readonly myDbContext _context;

    public BooksLibraryRepository(myDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BooksLibrary>> GetAllBooks()
    {
        return await _context.BooksLibrary.ToListAsync();
    }

    public async Task<BooksLibrary> GetBookById(int bookId)
    {
        return await _context.BooksLibrary.FindAsync(bookId);
    }

    public async Task AddBook(BooksLibrary book)
    {
        _context.BooksLibrary.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBook(BooksLibrary book)
    {
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBook(int bookId)
    {
        var book = await _context.BooksLibrary.FindAsync(bookId);
        _context.BooksLibrary.Remove(book);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<BooksLibrary>> SearchBooks(string searchTerm)
    {
        return await _context.BooksLibrary
            .Where(b => EF.Functions.Like(
                $"{b.Title} {b.Author} {b.Description}",
                $"%{searchTerm}%"))
            .ToListAsync();
    }
    public async Task<BooksLibrary> GetRecentlyAddedBook()
    {
        return await _context.Set<BooksLibrary>()
            .OrderByDescending(b => b.BookId)
            .FirstOrDefaultAsync();
    }
}