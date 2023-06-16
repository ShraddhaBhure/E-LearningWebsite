using C_Data;
using C_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C_Services
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly myDbContext _dbContext;

        public FeedbackRepository(myDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Feedback> GetByIdAsync(int id)
        {
            return await _dbContext.Feedback.FindAsync(id);
        }

        public IQueryable<Feedback> GetAll()
        {
            return _dbContext.Feedback;
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return await _dbContext.Feedback.ToListAsync();
        }

        public async Task InsertAsync(Feedback feedback)
        {
            _dbContext.Feedback.Add(feedback);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Feedback feedback)
        {
            _dbContext.Feedback.Update(feedback);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var feedback = await _dbContext.Feedback.FindAsync(id);
            if (feedback != null)
            {
                _dbContext.Feedback.Remove(feedback);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SaveImageAsync(int id, string imagePath)
        {
            var feedback = await _dbContext.Feedback.FindAsync(id);
            if (feedback != null)
            {
                feedback.ClientImage = imagePath;
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
