using C_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
