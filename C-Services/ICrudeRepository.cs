using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C_Services
{
    public interface ICrudeRepository<TEntity> where TEntity : class
        {
            Task<TEntity> GetByIdAsync(Guid id);
            IQueryable<TEntity> GetAll();
            Task<IEnumerable<TEntity>> GetAllAsync();
            Task InsertAsync(TEntity entity);
            Task UpdateAsync(TEntity entity);
            Task DeleteAsync(Guid id);
           Task SaveImageAsync(Guid id, IFormFile image);
          bool IsValidAdmin(string userName, string password);
        //object GetByIdAsync(int userId);
    }

}
