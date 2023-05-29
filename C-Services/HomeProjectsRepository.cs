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
    public class HomeProjectsRepository : IHomeProjectsRepository
    {
        private readonly myDbContext _context;

        public HomeProjectsRepository(myDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HomeProjects>> GetAllProjects()
        {
            return await _context.HomeProjects.ToListAsync();
        }

        public async Task<HomeProjects> GetProjectById(int projectId)
        {
            return await _context.HomeProjects.FindAsync(projectId);
        }

        public async Task AddProject(HomeProjects project)
        {
            _context.HomeProjects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProject(HomeProjects project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProject(int projectId)
        {
            var project = await _context.HomeProjects.FindAsync(projectId);
            _context.HomeProjects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}