using C_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Services
{
    public interface IHomeProjectsRepository
    {
        Task<IEnumerable<HomeProjects>> GetAllProjects();
        Task<HomeProjects> GetProjectById(int projectId);
        Task AddProject(HomeProjects project);
        Task UpdateProject(HomeProjects project);
        Task DeleteProject(int projectId);
    }
}
