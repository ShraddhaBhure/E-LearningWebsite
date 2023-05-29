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
    public class OnlineClassRepository : IOnlineClassRepository
    {
        private readonly myDbContext _context;

        public OnlineClassRepository(myDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OnlineClass>> GetAllClasses()
        {
            return await _context.OnlineClass.ToListAsync();
        }

        public async Task<OnlineClass> GetClassById(int classId)
        {
            return await _context.OnlineClass.FindAsync(classId);
        }

        public async Task AddClass(OnlineClass onlineClass)
        {
            _context.OnlineClass.Add(onlineClass);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClass(OnlineClass onlineClass)
        {
            _context.Entry(onlineClass).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClass(int classId)
        {
            var onlineClass = await _context.OnlineClass.FindAsync(classId);
            _context.OnlineClass.Remove(onlineClass);
            await _context.SaveChangesAsync();
        }
    }
}

