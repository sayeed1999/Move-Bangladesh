using Microsoft.EntityFrameworkCore;
using Sayeed.NTier.Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sayeed.NTier.Generic.Logic
{
    public class BaseService<T> : IBaseService<T> 
        where T : class
    {
        protected IBaseRepository<T> _repository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _repository = baseRepository;

        }

        /// <summary>
        /// Need to be called after certain operations on db: add update delete, otherwise changes will not be saved..
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveChangesAsync()
        {
            return await _repository.SaveChangesAsync();
        }

        #region basic crud operations

        public virtual async Task<IEnumerable<T>> GetAllAsync(int page = 1, int pageSize = 10)
        {
            return await _repository.GetAllAsync(page: page, pageSize: pageSize);
        }

        public virtual async Task<T> FindByIdAsync(long id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public virtual async Task AddAsync(T item)
        {
            await _repository.AddAsync(item);
            await this.SaveChangesAsync();
        }

        public virtual async Task UpdateByIdAsync(long id, T item)
        {
            _repository.UpdateById(id, item);
            await this.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T item)
        {
            _repository.Update(item);
            await this.SaveChangesAsync();
        }

        public virtual async Task Delete(T item)
        {
            _repository.Delete(item);
            await this.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(long id)
        {
            await _repository.DeleteByIdAsync(id);
            await this.SaveChangesAsync();
        }

        #endregion

    }
}
