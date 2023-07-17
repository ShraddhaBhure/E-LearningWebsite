using C_Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace C_Services
{
    public class CrudeRepository<TEntity> : ICrudeRepository<TEntity> where TEntity : class
        {
            private readonly myDbContext _dbContext;
            private readonly DbSet<TEntity> _dbSet;

            public CrudeRepository(myDbContext dbContext)
            {
                _dbContext = dbContext;
                _dbSet = _dbContext.Set<TEntity>();
            }
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
                return await _dbSet.FindAsync(id);
        }

            public async Task<IEnumerable<TEntity>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }

            public async Task InsertAsync(TEntity entity)
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }

            public async Task UpdateAsync(TEntity entity)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

            public async Task DeleteAsync(Guid id)
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }

        public async Task SaveImageAsync(Guid id, IFormFile image)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        entity.GetType().GetProperty("CoverImage").SetValue(entity, memoryStream.ToArray());
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
        }
        public bool IsValidAdmin(string username, string password)
        {
            // Check if the username and password are valid
            var user = _dbContext.Login.FirstOrDefault(u => u.UserName == username && u.UserPassword == password);

            // Return true if the user is valid and has the UserRole set to "Admin"
            if (user != null && user.UserRole == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
