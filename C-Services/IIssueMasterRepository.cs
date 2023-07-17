using C_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Services
{
    public interface IIssueMasterRepository
    {
        Task<IEnumerable<IssueMaster>> GetAllIssues();
        Task<IssueMaster> GetIssueById(int id);
        Task CreateIssue(IssueMaster issue);
        Task UpdateIssue(IssueMaster issue);
        Task DeleteIssue(int id);
        Task<IssueMaster> GetRecentlyAddedIssue();
        Task<IEnumerable<IssueMaster>> SearchIssues(string searchTerm);
       
        ////UploadArticles
       
        Task<List<string>> GetDisplay1Titles();
        Task<List<string>> GetDisplay2Titles();
        Task CreateArticle(ArticleUpload articleUpload);


    }

}
