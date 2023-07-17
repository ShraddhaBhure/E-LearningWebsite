using C_Data;
using C_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Services
{
    public class IssueMasterRepository : IIssueMasterRepository
    {
        private readonly myDbContext _context;

        public IssueMasterRepository(myDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IssueMaster>> GetAllIssues()
        {
            return await _context.IssueMaster.ToListAsync();
        }

        public async Task<IssueMaster> GetIssueById(int id)
        {
            return await _context.IssueMaster.FindAsync(id);
        }



        public async Task CreateIssue(IssueMaster issue)
        {
            issue.IssueDate = DateTime.Now;
            _context.IssueMaster.Add(issue);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIssue(IssueMaster issue)
        {
            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIssue(int id)
        {
            var issue = await _context.IssueMaster.FindAsync(id);
            if (issue != null)
            {
                _context.IssueMaster.Remove(issue);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<IssueMaster>> SearchIssues(string searchTerm)
        {
            return await _context.IssueMaster
                .Where(i => EF.Functions.Like(
                    $"{i.IssueName} {i.IssueDescription}",
                    $"%{searchTerm}%"))
                .ToListAsync();
        }


        public async Task<IssueMaster> GetRecentlyAddedIssue()
        {
            return await _context.Set<IssueMaster>()
                .OrderByDescending(i => i.IssueID)
                .FirstOrDefaultAsync();
        }
        ///-----------UploadArticles
        public async Task<List<string>> GetDisplay1Titles()
        {
            return await _context.IssueMaster.Select(i => i.Display1Title).ToListAsync();
        }

        public async Task<List<string>> GetDisplay2Titles()
        {
            return await _context.IssueMaster.Select(i => i.Display2Title).ToListAsync();
        }

        public async Task CreateArticle(ArticleUpload articleUpload)
        {
            _context.ArticleUpload.Add(articleUpload);
            await _context.SaveChangesAsync();
        }
    }

}
