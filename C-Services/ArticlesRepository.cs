using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using C_Data;
using C_Models;

namespace C_Services
{

    public class ArticlesRepository : IArticlesRepository
    {
        private readonly myDbContext _context;

        public ArticlesRepository(myDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<Article> GetArticleById(int articleId)
        {
            return await _context.Articles.FindAsync(articleId);
        }

        public async Task AddArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArticle(Article article)
        {
            _context.Entry(article).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArticle(int articleId)
        {
            var article = await _context.Articles.FindAsync(articleId);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<Article>> SearchArticles(string searchTerm)
        //{
        //    return await _context.Articles
        //        .Where(a => EF.Functions.Like(
        //            $"{a.Title} {a.Author} {a.Content}",
        //            $"%{searchTerm}%"))
        //        .ToListAsync();
        //}

        public async Task<Article> GetRecentlyAddedArticle()
        {
            return await _context.Set<Article>()
                .OrderByDescending(a => a.ArticleID)
                .FirstOrDefaultAsync();
        }

       




    }

   
}
