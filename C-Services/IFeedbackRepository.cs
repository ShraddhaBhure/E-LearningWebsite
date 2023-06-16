using C_Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C_Services
{
    public interface IFeedbackRepository
    {
        Task<Feedback> GetByIdAsync(int id);
        IQueryable<Feedback> GetAll();
        Task<IEnumerable<Feedback>> GetAllAsync();
        Task InsertAsync(Feedback feedback);
        Task UpdateAsync(Feedback feedback);
        Task DeleteAsync(int id);
        Task SaveImageAsync(int id, string imagePath);
    }
}
