using C_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C_Services
{
    public interface IArticlesRepository
    {
        Task<IEnumerable<Article>> GetAllArticles();
        Task<Article> GetArticleById(int articleId);
        Task AddArticle(Article article);
        Task UpdateArticle(Article article);
        Task DeleteArticle(int articleId);
     //   Task<IEnumerable<Article>> SearchArticles(string searchTerm);
        Task<Article> GetRecentlyAddedArticle();
      //  Task<Article> DownloadArticleFile(int articleId);
    }
}
