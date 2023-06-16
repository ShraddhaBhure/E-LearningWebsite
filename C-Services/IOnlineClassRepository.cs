using C_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C_Services
{
    public interface IOnlineClassRepository
    {
        Task<IEnumerable<OnlineClass>> GetAllClasses();
        Task<OnlineClass> GetClassById(int classId);
        Task AddClass(OnlineClass onlineClass);
        Task UpdateClass(OnlineClass onlineClass);
        Task DeleteClass(int classId);
    }
}
